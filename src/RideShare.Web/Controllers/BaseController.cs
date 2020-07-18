using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RideShare.Web.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Controllers
{
    [ApiController]
    [Route("v{ver:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        /// <inheritdoc/>
        [NonAction]
        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            var mainErrors = modelState.Where(x => !string.IsNullOrEmpty(x.Key)).ToDictionary(k => k.Key, v => v.Value.Errors.Select(x => x.ErrorMessage));

            return base.BadRequest(new ApiResult
            {
                Success = false,
                Data = mainErrors,
                Key = "ModelState",
                Message = mainErrors != null ? string.Join("\n", mainErrors.SelectMany(x => x.Value)) : null,
            });
        }

        /// <inheritdoc/>
        [NonAction]
        public override BadRequestObjectResult BadRequest(object data)
        {
            return base.BadRequest(new ApiResult
            {
                Success = false,
                Data = data,
                Key = "BadRequest",
            });
        }

        /// <summary>
        /// Returns badrequest with key.
        /// </summary>
        /// <param name="data">Data to return.</param>
        /// <param name="key">Key to response.</param>
        /// <returns>BadRequestObjectResult.</returns>
        [NonAction]
        public BadRequestObjectResult BadRequest(object data, string key)
        {
            return base.BadRequest(new ApiResult
            {
                Success = false,
                Data = data,
                Key = key,
            });
        }

        [NonAction]
        public BadRequestObjectResult BadRequest(IdentityResult result)
        {
            var first = result.Errors?.FirstOrDefault();
            return base.BadRequest(new ApiResult
            {
                Success = false,
                Key = first?.Code ?? "IdentityResult",
                Message = first.Description,
                Data = result,
            });
        }

        /// <inheritdoc/>
        [NonAction]
        public override NotFoundObjectResult NotFound(object value)
        {
            return base.NotFound(new ApiResult
            {
                Success = false,
                Key = "NotFound",
                Data = value,
            });
        }

        /// <inheritdoc/>
        [NonAction]
        public override AcceptedResult Accepted(object value)
        {
            return base.Accepted(new ApiResult
            {
                Success = true,
                Key = "Accepted",
                Data = value,
            });
        }

        /// <inheritdoc/>
        [NonAction]
        public override AcceptedResult Accepted(Uri uri, object value)
        {
            return base.Accepted(uri, new ApiResult
            {
                Success = true,
                Key = "Accepted",
                Data = value,
            });
        }

        public override CreatedResult Created(string uri, [ActionResultObjectValue] object value)
        {
            return base.Created(uri, new ApiResult
            {
                Data = value,
                Success = true,
            });
        }

        /// <inheritdoc/>
        public override OkObjectResult Ok(object value)
        {
            if (value == null)
                return base.Ok(new ApiResult
                {
                    Success = true,
                    Data = new object(),
                    Message = "Data is empty"
                });

            return base.Ok(new ApiResult
            {
                Success = true,
                Data = value,
            });
        }

        [NonAction]
        public OkObjectResult Ok(object value, object meta)
        {
            return base.Ok(new ApiResult
            {
                Success = true,
                Data = value,
                Meta = meta,
            });
        }


        [NonAction]
        public OkObjectResult Ok<T>(IList<T> listResult, int count)
        {
            return this.Ok(
                listResult,
                new CollectionMetaData(count));
        }

        public override CreatedAtActionResult CreatedAtAction(string actionName, string controllerName, object routeValues, [ActionResultObjectValue] object value)
        {
            return base.CreatedAtAction(actionName, controllerName, routeValues, new ApiResult
            {
                Success = true,
                Data = value,
            });
        }

        public override CreatedAtRouteResult CreatedAtRoute(string routeName, object routeValues, [ActionResultObjectValue] object value)
        {
            return base.CreatedAtRoute(routeName, routeValues, new ApiResult
            {
                Success = true,
                Data = value,
            });
        }

        /// <summary>
        /// This class converts given type to generic <see cref="ApiResult{T}"/>.
        /// </summary>
        public new class ProducesResponseTypeAttribute : Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ProducesResponseTypeAttribute"/> class.
            /// </summary>
            /// <param name="statusCode">Http Status Code to return.</param>
            public ProducesResponseTypeAttribute(int statusCode) : base(typeof(ApiResult), statusCode)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ProducesResponseTypeAttribute"/> class.
            /// </summary>
            /// <param name="type">Type of response. (without <see cref="ApiResult{T}"/>.)</param>
            /// <param name="statusCode">Http Status Code to return.</param>
            public ProducesResponseTypeAttribute(Type type, int statusCode) : base(type.IsArray ? typeof(ApiResult<,>).MakeGenericType(type, typeof(CollectionMetaData)) : typeof(ApiResult<>).MakeGenericType(type), statusCode)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ProducesResponseTypeAttribute"/> class.
            /// </summary>
            /// <param name="type">Type of response. (without <see cref="ApiResult{T}"/>.)</param>
            /// <param name="statusCode">Http Status Code to return.</param>
            public ProducesResponseTypeAttribute(Type dataType, Type metaType, int statusCode) : base(typeof(ApiResult<,>).MakeGenericType(dataType, metaType), statusCode)
            {
            }
        }

        public new class SwaggerResponseExampleAttribute : Swashbuckle.AspNetCore.Filters.SwaggerResponseExampleAttribute
        {
            public SwaggerResponseExampleAttribute(int statusCode, Type examplesProviderType, Type contractResolver = null, Type jsonConverter = null) : base(statusCode, examplesProviderType.IsArray ? typeof(ApiResult<,>).MakeGenericType(examplesProviderType, typeof(CollectionMetaData)) : typeof(ApiResult<>).MakeGenericType(examplesProviderType), contractResolver, jsonConverter)
            {

            }
        }
        public new class SwaggerRequestExampleAttribute : Swashbuckle.AspNetCore.Filters.SwaggerRequestExampleAttribute
        {
            public SwaggerRequestExampleAttribute(Type requestType, Type examplesProviderType, Type contractResolver = null, Type jsonConverter = null) : base(requestType, examplesProviderType.IsArray ? typeof(ApiResult<,>).MakeGenericType(examplesProviderType, typeof(CollectionMetaData)) : typeof(ApiResult<>).MakeGenericType(examplesProviderType), contractResolver, jsonConverter)
            {
            }
        }
    }
}
