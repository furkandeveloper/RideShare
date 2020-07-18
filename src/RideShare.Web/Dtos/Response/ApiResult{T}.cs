
namespace RideShare.Web.Dtos.Response
{
    /// <summary>
    /// This is base class of Api Response models. All requests will return in this template. See BaseController for implementation.
    /// </summary>
    /// <typeparam name="T">Type of data.</typeparam>
    public class ApiResult<T> : ApiResult<T, object>
    {
    }
}
