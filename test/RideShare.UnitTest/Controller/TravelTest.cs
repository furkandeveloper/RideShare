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
    public class TravelTest
    {
        private readonly string userId = Guid.NewGuid().ToString();
        private readonly string travelId = Guid.NewGuid().ToString();

        [Theory, AutoMoqData]
        public async Task InsertTravelAsync_Return_Created_Result(Mock<ITravelService> userService,
                                                               TravelResponseDto expected)
        {
            // Arrange
            var sut = new TravelController(userService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            TravelRequestDto requestDto = new TravelRequestDto();
            userService.Setup(setup => setup.InsertTravelAsync(requestDto)).Returns(Task.FromResult(expected));

            // Act
            var result = sut.InsertTravelAsync(requestDto);

            var apiResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var model = Assert.IsType<ApiResult>(apiResult.Value);
            TravelResponseDto response = model.Data as TravelResponseDto;

            // Assert

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);

            Assert.NotNull(result.Result);
            Assert.NotNull(expected);
            Assert.IsAssignableFrom<TravelResponseDto>(expected);
        }

        [Theory, AutoMoqData]
        public async Task FilterTravelAsync_Return_Ok_Result(Mock<ITravelService> travelService,
                                                               List<TravelResponseDto> expected)
        {
            // Arrange
            var sut = new TravelController(travelService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            TravelFilterDto dto = new TravelFilterDto();
            travelService.Setup(setup => setup.FilterTravelAsync(dto)).Returns(Task.FromResult(expected));

            // Act
            var result = sut.TravelFilterAsync(dto);

            var apiResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var model = Assert.IsType<ApiResult>(apiResult.Value);
            List<TravelResponseDto> response = model.Data as List<TravelResponseDto>;

            // Assert

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);

            Assert.NotNull(result.Result);
            Assert.NotNull(expected);
            Assert.IsAssignableFrom<List<TravelResponseDto>>(expected);
        }

        [Theory, AutoMoqData]
        public async Task UpdateTravelAsync_Return_NoContent_Result(Mock<ITravelService> travelService)
        {
            var sut = new TravelController(travelService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            TravelUpdateRequestDto dto = new TravelUpdateRequestDto();
            travelService.Setup(setup => setup.ReplaceTravelAsync(travelId, dto));

            // Act
            var result = sut.UpdateTravelAsync(travelId,dto);


            // Assert

            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);
        }

        [Theory, AutoMoqData]
        public async Task DeleteTravelAsync_Return_NoContent_Result(Mock<ITravelService> travelService)
        {
            var sut = new TravelController(travelService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            travelService.Setup(setup => setup.DeleteTravelAsync(travelId));

            // Act
            var result = sut.DeleteTravelAsync(travelId);

            // Assert

            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);
        }
        [Theory, AutoMoqData]
        public async Task ActiveTravelAsync_Return_NoContent_Result(Mock<ITravelService> travelService)
        {
            var sut = new TravelController(travelService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            travelService.Setup(setup => setup.ActiveTravelAsync(travelId));

            // Act
            var result = sut.ActiveTravelAsync(travelId);

            // Assert

            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);
        }

        [Theory, AutoMoqData]
        public async Task PassiveTravelAsync_Return_NoContent_Result(Mock<ITravelService> travelService)
        {
            var sut = new TravelController(travelService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            travelService.Setup(setup => setup.PassiveTravelAsync(travelId));

            // Act
            var result = sut.PassiveTravelAsync(travelId);

            // Assert

            Assert.IsType<NoContentResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);
        }

        [Theory, AutoMoqData]
        public async Task IncludeTravelAsync_Return_Ok_Result(Mock<ITravelService> travelService,
                                                               TravelResponseDto expected)
        {
            // Arrange
            var sut = new TravelController(travelService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            travelService.Setup(setup => setup.IncludeTravelAsync(userId,travelId)).Returns(Task.FromResult(expected));

            // Act
            var result = sut.IncludeTravelAsync(userId,travelId);

            var apiResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var model = Assert.IsType<ApiResult>(apiResult.Value);
            TravelResponseDto response = model.Data as TravelResponseDto;

            // Assert

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);

            Assert.NotNull(result.Result);
            Assert.NotNull(expected);
            Assert.IsAssignableFrom<TravelResponseDto>(expected);
        }

        [Theory, AutoMoqData]
        public async Task LeaveTravelAsync_Return_Ok_Result(Mock<ITravelService> travelService,
                                                              TravelResponseDto expected)
        {
            // Arrange
            var sut = new TravelController(travelService.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            travelService.Setup(setup => setup.LeaveTravelAsync(userId,travelId)).Returns(Task.FromResult(expected));

            // Act
            var result = sut.LeaveTravelAsync(userId,travelId);

            var apiResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var model = Assert.IsType<ApiResult>(apiResult.Value);
            TravelResponseDto response = model.Data as TravelResponseDto;

            // Assert

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsNotType<CreatedAtActionResult>(result.Result);
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<AcceptedAtActionResult>(result.Result);

            Assert.NotNull(result.Result);
            Assert.NotNull(expected);
            Assert.IsAssignableFrom<TravelResponseDto>(expected);
        }
    }
}
