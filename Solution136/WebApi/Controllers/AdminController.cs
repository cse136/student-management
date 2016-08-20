namespace WebApi.Controllers
{
    using System.Web.Http;
    using POCO;

    public class AdminController : ApiController
    {
        [HttpGet]
        public admin GetAdminInfo(int adminId)
        {
            //// 136 TODO: get the admin info 
            //// for now, returning the hard-coded value
            return new admin() { FirstName = "Isaac", LastName = "Chu", Id = adminId };
        }

        [HttpPost]
        public string UpdateAdminInfo(admin admin)
        {
            //// 136 TODO : update admin info here...
            return "update not yet implemented";
        }
    }
}