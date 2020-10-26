using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pri.Identity.Web.Pages
{
    [Authorize]
    public class PrivatePageStudentModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}