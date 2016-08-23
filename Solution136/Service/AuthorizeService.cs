namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using IRepository;
    using POCO;
    
    public class AuthorizeService
    {
        private readonly IAuthorizeRepository repository;

        public AuthorizeService(IAuthorizeRepository repository)
        {
            this.repository = repository;
        }

        public Logon Authenticate(string email, string password, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                errors.Add("Invalid email or password.");
                throw new ArgumentException();
            }

            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!isEmail)
            {
                errors.Add("Please check the email format.");
                throw new ArgumentException();
            }

            return this.repository.Authenticate(email, password, ref errors);
        }
    }
}
