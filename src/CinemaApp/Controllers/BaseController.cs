using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaApp.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected bool isUserAuthenticated()
        {
            if (User == null)
            {
                return false;
            }

            if (User.Identity == null)
            {
                return false;
            }

            return User.Identity.IsAuthenticated;
        }
    }
}
