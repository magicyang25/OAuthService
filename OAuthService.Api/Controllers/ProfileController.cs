
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace OAuthService.Client.Controllers
{
    public class ProfileController : ApiController
    {
        [HttpGet]
        [ScopeAuthorize("read")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var data = new { id = id, name = "name", guid = Guid.NewGuid() };
            var claimPrinciple = User as ClaimsPrincipal;
            
            return Ok(data);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody]string value)
        {
            var data = new { value = value, name = "name", guid = Guid.NewGuid() };
            return Ok(data);
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