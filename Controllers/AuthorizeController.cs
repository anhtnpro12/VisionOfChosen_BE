using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VisionOfChosen_BE.Authentication;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorizeController : Controller
    {
        protected UserHeader UserHeader
        {
            get
            {
                try
                {
                    return new UserHeader
                    {
                        UserId = User.FindFirst(CustomClaimTypes.UserId)?.Value ?? "",
                        Email = User.FindFirst(CustomClaimTypes.Email)?.Value ?? "",
                        Role = User.FindFirst(CustomClaimTypes.Role)?.Value ?? "",
                        Name = User.FindFirst(CustomClaimTypes.Name)?.Value ?? "",
                    };
                }
                catch
                {
                    return new UserHeader();
                }
            }
        }
    }
}
