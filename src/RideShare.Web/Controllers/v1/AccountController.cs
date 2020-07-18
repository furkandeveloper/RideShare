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
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}",Name ="GetUserInformation")]
        [ProducesResponseType(typeof(UserResponseDto),200)]
        public async Task<IActionResult> GetUserInformationAsync(string userId)
        {
            return Ok(await userService.GetUserInformationAsync(userId));
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(Name ="RegisterUser")]
        [ProducesResponseType(typeof(UserResponseDto), 201)]
        [ModelStateFilter]
        public async Task<IActionResult> RegisterUserAsync([FromBody]UserRequestDto model)
        {
            return CreatedAtAction(nameof(GetUserInformationAsync),Request.RouteValues,await userService.RegisterUserAsync(model));
        }

        /// <summary>
        /// Update User information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("user/{userId}",Name = "UpdateUser")]
        [ProducesResponseType(204)]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateUserAsync(string userId,[FromBody] UserUpdateRequestDto model)
        {
            await userService.ReplaceUserAsync(userId, model);
            return NoContent();
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("user/{userId}", Name = "DeleteUser")]
        [ProducesResponseType(204)]
        [ModelStateFilter]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            await userService.DeleteUserAsync(userId);
            return NoContent();
        }

        /// <summary>
        /// Active User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("user/{userId}/active", Name = "ActiveUser")]
        [ProducesResponseType(204)]
        [ModelStateFilter]
        public async Task<IActionResult> ActiveUserAsync(string userId)
        {
            await userService.ActiveUserAsync(userId);
            return NoContent();
        }

        /// <summary>
        /// Passive User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("user/{userId}/passive", Name = "PassiveUser")]
        [ProducesResponseType(204)]
        [ModelStateFilter]
        public async Task<IActionResult> PassiveUserAsync(string userId)
        {
            await userService.PassiveUserAsync(userId);
            return NoContent();
        }
    }
}
