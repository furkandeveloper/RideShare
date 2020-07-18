using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RideShare.UnitTest.Static;
using RideShare.Web.Controllers.v1;
using RideShare.Web.Dtos.Request;
using RideShare.Web.Dtos.Response;
using RideShare.Web.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RideShare.UnitTest.Controller
{
    public class AccountTest
    {
        private readonly string userId = Guid.NewGuid().ToString();

        [Theory, AutoMoqData]
        public async Task InsertBrandAsync_Return_Created_Result(Mock<IUserService> userService,
                                                               UserResponseDto expected)
        {
            // Arrange
            var sut = new AccountController(userService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            UserRequestDto requestDto = new UserRequestDto() { Name = "testName", Surname = "testSurname"};
            userService.Setup(setup => setup.RegisterUserAsync(requestDto)).Returns(Task.FromResult(expected));

            // Act
            var result = sut.RegisterUserAsync(requestDto);

            var apiResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var model = Assert.IsType<ApiResult>(apiResult.Value);
            UserResponseDto response = model.Data as UserResponseDto;

            // Assert

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);

            Assert.NotNull(result.Result);
            Assert.NotNull(expected);
            Assert.IsAssignableFrom<UserResponseDto>(expected);
        }

        [Theory, AutoMoqData]
        public async Task GetBrandAsync_Return_Ok_Result(Mock<IUserService> userService,
                                                               UserResponseDto expected)
        {
            // Arrange
            var sut = new AccountController(userService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            userService.Setup(setup => setup.GetUserInformationAsync(userId)).Returns(Task.FromResult(expected));

            // Act
            var result = sut.GetUserInformationAsync(userId);

            var apiResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var model = Assert.IsType<ApiResult>(apiResult.Value);
            UserResponseDto response = model.Data as UserResponseDto;

            // Assert

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);

            Assert.NotNull(result.Result);
            Assert.NotNull(expected);
            Assert.IsAssignableFrom<UserResponseDto>(expected);
        }

        [Theory, AutoMoqData]
        public async Task UpdateBrandAsync_Return_NoContent_Result(Mock<IUserService> userService)
        {
            // Arrange
            var sut = new AccountController(userService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            UserUpdateRequestDto dto = new UserUpdateRequestDto();
            userService.Setup(setup => setup.ReplaceUserAsync(userId,dto));

            // Act
            var result = sut.UpdateUserAsync(userId,dto);


            // Assert
            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);            
        }

        [Theory, AutoMoqData]
        public async Task DeleteBrandAsync_Return_NoContent_Result(Mock<IUserService> userService)
        {
            // Arrange
            var sut = new AccountController(userService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            userService.Setup(setup => setup.DeleteUserAsync(userId));

            // Act
            var result = sut.DeleteUserAsync(userId);


            // Assert
            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);
        }

        [Theory, AutoMoqData]
        public async Task ActiveBrandAsync_Return_NoContent_Result(Mock<IUserService> userService)
        {
            // Arrange
            var sut = new AccountController(userService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            userService.Setup(setup => setup.ActiveUserAsync(userId));

            // Act
            var result = sut.ActiveUserAsync(userId);


            // Assert
            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);
        }

        [Theory, AutoMoqData]
        public async Task PassiveBrandAsync_Return_NoContent_Result(Mock<IUserService> userService)
        {
            // Arrange
            var sut = new AccountController(userService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            userService.Setup(setup => setup.PassiveUserAsync(userId));

            // Act
            var result = sut.PassiveUserAsync(userId);


            // Assert
            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);
        }
    }
}
