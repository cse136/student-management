namespace SecuredWebApi.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Http;

    public class ProfilesController : ApiController
    {
        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            var user = (ClaimsPrincipal)User;

            var email = user.Claims
                .FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Value;

            if (email == null)
            {
                return this.NotFound();
            }

            var returnData = "I can hit the secured web api";
            return this.Ok(returnData);
        }
    }
}