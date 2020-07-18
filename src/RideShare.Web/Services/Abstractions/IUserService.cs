using Microsoft.Extensions.Configuration.UserSecrets;
using RideShare.Web.Dtos.Request;
using RideShare.Web.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterUserAsync([NotNull]UserRequestDto dto);

        Task<UserResponseDto> GetUserInformation([NotNull] string userId);

        Task DeleteUserAsync([NotNull] string userId);

        Task ReplaceUserAsync([NotNull] string userId, [NotNull] UserUpdateRequestDto dto);

        Task ActiveUserAsync([NotNull]string userId);
        Task PassiveUserAsync([NotNull] string userId);
    }
}
