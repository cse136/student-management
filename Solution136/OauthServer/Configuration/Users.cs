namespace OauthServer.Configuration
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using IdentityServer3.Core;
    using IdentityServer3.Core.Services.InMemory;

    public class Users
    {
        public static List<InMemoryUser> GetUsers()
        {
            // in real world, this would be a database call...
            // this is simplified by hard coding values for demo purposes.
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "ichu@eng.ucsd.edu",
                    Username = "ichu@eng.ucsd.edu",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.Name, "Isaac Chu")
                    }
                }
            };
        }
    }
}