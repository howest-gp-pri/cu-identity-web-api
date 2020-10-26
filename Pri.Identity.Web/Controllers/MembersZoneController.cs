using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pri.Identity.Web.Controllers
{
    [Authorize]
    public class MembersZoneController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberBenefits()
        {
            return View();
        }
    }
}