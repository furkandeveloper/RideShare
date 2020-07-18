using AutoMapper;
using Microsoft.Extensions.Configuration;
using RideShare.Web.Dtos.Request;
using RideShare.Web.Dtos.Response;
using RideShare.Web.Entities;
using RideShare.Web.Repositories.Abstractions;
using RideShare.Web.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public UserManager(IMapper mapper,IUserRepository userRepository,IConfiguration configuration)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        private async Task<User> FindUserAsync([NotNull]string userId, bool isActive = true)
        {
            return await userRepository.GetSingleAsync(x => x.Id == userId && x.IsActive == isActive) ?? throw new Exception("EntityNotFound");
        }

        public async Task ActiveUserAsync([NotNull] string userId)
        {
            var user = await FindUserAsync(userId, false);
            user.IsActive = true;
            user.UpdateDate = DateTime.UtcNow;
            await userRepository.ReplaceAsync(user);
        }

        public async Task DeleteUserAsync([NotNull] string userId)
        {
            var user = await FindUserAsync(userId);
            await userRepository.DeleteAsync(user);
        }

        public async Task<UserResponseDto> GetUserInformation([NotNull] string userId)
        {
            var user = await FindUserAsync(userId);
            var mapped = mapper.Map<UserResponseDto>(user);
            mapped.AccessToken = configuration.GetValue<string>("AccessToken");
            return mapped;
        }

        public async Task PassiveUserAsync([NotNull] string userId)
        {
            var user = await FindUserAsync(userId);
            user.IsActive = false;
            user.UpdateDate = DateTime.UtcNow;
            await userRepository.ReplaceAsync(user);
        }

        public async Task<UserResponseDto> RegisterUserAsync([NotNull] UserRequestDto dto)
        {
            var response = mapper.Map<UserResponseDto>(await userRepository.AddAsync(mapper.Map<User>(dto)));
            response.AccessToken = configuration.GetValue<string>("AccessToken");
            return response;
        }

        public async Task ReplaceUserAsync([NotNull] string userId, [NotNull] UserUpdateRequestDto dto)
        {
            var user = await FindUserAsync(userId);
            var mapped = mapper.Map(dto, user);
            user.UpdateDate = DateTime.UtcNow;
            await userRepository.ReplaceAsync(mapped);
        }
    }
}
