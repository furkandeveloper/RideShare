using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    }
}
