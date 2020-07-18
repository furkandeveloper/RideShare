using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideShare.Web.Dtos.Request;
using RideShare.Web.Dtos.Response;
using RideShare.Web.Helpers.Filters;
using RideShare.Web.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TravelController : BaseController
    {
        private readonly ITravelService travelService;

        public TravelController(ITravelService travelService)
        {
            this.travelService = travelService;
        }

        /// <summary>
        /// Filter Travels
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet(Name ="FilterTravel")]
        [ProducesResponseType(typeof(TravelResponseDto[]),200)]
        public async Task<IActionResult> TravelFilterAsync([FromQuery]TravelFilterDto filter)
        {
            return Ok(await travelService.FilterTravelAsync(filter));
        }

        /// <summary>
        /// Insert Travel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(Name ="InsertTravel")]
        [ProducesResponseType(typeof(TravelResponseDto),201)]
        [ModelStateFilter]
        public async Task<IActionResult> InsertTravelAsync([FromBody] TravelRequestDto model)
        {
            return CreatedAtAction(nameof(TravelFilterAsync), Request.RouteValues, await travelService.InsertTravelAsync(model));
        }

        /// <summary>
        /// Update travel
        /// </summary>
        /// <param name="travelId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("travel/{travelId}",Name ="")]
        [ProducesResponseType(204)]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateTravelAsync(string travelId, [FromBody]TravelUpdateRequestDto model)
        {
            await travelService.ReplaceTravelAsync(travelId, model);
            return NoContent();
        }

        /// <summary>
        /// Active Travel
        /// </summary>
        /// <param name="travelId"></param>
        /// <returns></returns>
        [HttpPut("travel/{travelId}/active",Name ="ActiveTravel")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ActiveTravelAsync(string travelId)
        {
            await travelService.ActiveTravelAsync(travelId);
            return NoContent();
        }

        /// <summary>
        /// Passive Travel
        /// </summary>
        /// <param name="travelId"></param>
        /// <returns></returns>
        [HttpPut("travel/{travelId}/Passive", Name = "PassiveTravel")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PassiveTravelAsync(string travelId)
        {
            await travelService.PassiveTravelAsync(travelId);
            return NoContent();
        }

        /// <summary>
        /// Delete Travel
        /// </summary>
        /// <param name="travelId"></param>
        /// <returns></returns>
        [HttpDelete("travel/{travelId}",Name ="DeleteTravel")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteTravelAsync(string travelId)
        {
            await travelService.DeleteTravelAsync(travelId);
            return NoContent();
        }

        /// <summary>
        /// Incude user of travel
        /// </summary>
        /// <param name="travelId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("travel/{travelId}/user/{userId}/include",Name ="IncludeTravel")]
        [ProducesResponseType(typeof(TravelResponseDto),200)]
        public async Task<IActionResult> IncludeTravelAsync(string travelId, string userId)
        {
            return Ok(await travelService.IncludeTravelAsync(userId, travelId));
        }

        /// <summary>
        /// Leave user of travel
        /// </summary>
        /// <param name="travelId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("travel/{travelId}/user/{userId}/leave", Name = "LeaveTravel")]
        [ProducesResponseType(typeof(TravelResponseDto), 200)]
        public async Task<IActionResult> LeaveTravelAsync(string travelId, string userId)
        {
            return Ok(await travelService.LeaveTravelAsync(userId, travelId));
        }
    }
}
