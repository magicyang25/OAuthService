using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OAuthService.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Private()
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", 
                    claimsPrincipal.FindFirst("access_token").Value);

                var profile = await client.GetAsync("http://localhost:61897/api/Profile/1");


                var profileContent = await profile.Content.ReadAsStringAsync();
            }

            return View(claimsPrincipal.Claims);
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();

            return Redirect("/");
        }
    }
}