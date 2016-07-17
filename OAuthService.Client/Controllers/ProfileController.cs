
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace OAuthService.Client.Controllers
{
    public class ProfileController : ApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var claimPrinciple = User as ClaimsPrincipal ;
            
            return Ok(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}