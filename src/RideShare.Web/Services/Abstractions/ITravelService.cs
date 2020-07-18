using RideShare.Web.Dtos.Request;
using RideShare.Web.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Services.Abstractions
{
    public interface ITravelService
    {
        Task<TravelResponseDto> InsertTravelAsync([NotNull]TravelRequestDto dto);

        Task<List<TravelResponseDto>> FilterTravelAsync([NotNull]TravelFilterDto filterDto);

        Task DeleteTravelAsync([NotNull]string travelId);

        Task ReplaceTravelAsync([NotNull]string travelId, [NotNull]TravelUpdateRequestDto dto);
        Task ActiveTravelAsync([NotNull] string travelId);
        Task PassiveTravelAsync([NotNull] string travelId);

        Task<TravelResponseDto> IncludeTravelAsync([NotNull]string userId,[NotNull]string travelId);

        Task<TravelResponseDto> LeaveTravelAsync([NotNull]string userId,[NotNull]string travelId);
    }
}
