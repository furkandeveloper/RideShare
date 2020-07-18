using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using RideShare.Web.Dtos.Request;
using RideShare.Web.Dtos.Response;
using RideShare.Web.Entities;
using RideShare.Web.Repositories.Abstractions;
using RideShare.Web.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RideShare.Web.Services.Concrete
{
    public class TravelManager : ITravelService
    {
        private readonly IMapper mapper;
        private readonly ITravelRepository travelRepository;
        private readonly IUserService userService;

        public TravelManager(IMapper mapper, ITravelRepository travelRepository,IUserService userService)
        {
            this.mapper = mapper;
            this.travelRepository = travelRepository;
            this.userService = userService;
        }

        private async Task<Travel> FindTravelAsync([NotNull]string travelId , bool isActive = true)
        {
            return await travelRepository.GetSingleAsync(x => x.Id == travelId && x.IsActive == isActive) ?? throw new Exception("EntityNotFound");
        }

        public async Task ActiveTravelAsync([NotNull] string travelId)
        {
            var travel = await FindTravelAsync(travelId, false);
            travel.IsActive = true;
            travel.UpdateDate = DateTime.UtcNow;
            await travelRepository.ReplaceAsync(travel);
        }

        public async Task DeleteTravelAsync([NotNull] string travelId)
        {
            var travel = await FindTravelAsync(travelId);
            if (travel.Users.Count > 0)
                throw new Exception("EntityNotDeleted");
            await travelRepository.DeleteAsync(travel);
        }

        public async Task<List<TravelResponseDto>> FilterTravelAsync([NotNull] TravelFilterDto filterDto)
        {
            return mapper.Map<List<TravelResponseDto>>(await travelRepository.FilterTravelAsync(filterDto));
        }

        public async Task<TravelResponseDto> InsertTravelAsync([NotNull] TravelRequestDto dto)
        {
            var mapped = mapper.Map<Travel>(dto);
            return mapper.Map<TravelResponseDto>(await travelRepository.AddAsync(mapped));
        }

        public async Task PassiveTravelAsync([NotNull] string travelId)
        {
            var travel = await FindTravelAsync(travelId);
            travel.IsActive = false;
            travel.UpdateDate = DateTime.UtcNow;
            await travelRepository.ReplaceAsync(travel);
        }

        public async Task ReplaceTravelAsync([NotNull] string travelId, [NotNull] TravelUpdateRequestDto dto)
        {
            var travel = await FindTravelAsync(travelId);
            var mapped = mapper.Map(dto, travel);
            mapped.UpdateDate = DateTime.UtcNow;
            if (mapped.TotalArmchair < travel.CurrentArmchair)
                throw new Exception("TotalArmchair");
            await travelRepository.ReplaceAsync(travel);
        }

        public async Task<TravelResponseDto> IncludeTravelAsync([NotNull] string userId,[NotNull]string travelId)
        {
            var user = await userService.GetUserInformationAsync(userId);
            var travel = await FindTravelAsync(travelId);
            if (travel.Users.Any(x => x.Id == userId))
                throw new Exception("Already Exist");
            if (travel.CurrentArmchair + 1 > travel.TotalArmchair)
                throw new Exception("Overflow armchair");
            travel.CurrentArmchair = travel.CurrentArmchair + 1;
            travel.Users.Add(mapper.Map<User>(user));
            await travelRepository.ReplaceAsync(travel);
            return mapper.Map<TravelResponseDto>(travel);
        }

        public async Task<TravelResponseDto> LeaveTravelAsync([NotNull] string userId,[NotNull]string travelId)
        {
            var user = await userService.GetUserInformationAsync(userId);
            var travel = await FindTravelAsync(travelId);
            if (!travel.Users.Any(x => x.Id == userId))
                throw new Exception("UserNotFound of travel");
            travel.Users.RemoveAll(x => x.Id == userId);
            travel.CurrentArmchair = travel.CurrentArmchair - 1;
            await travelRepository.ReplaceAsync(travel);
            return mapper.Map<TravelResponseDto>(travel);
        }
    }
}
