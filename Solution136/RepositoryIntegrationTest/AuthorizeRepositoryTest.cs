namespace RepositoryIntegrationTest
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repository;

    [TestClass]
    public class AuthorizeRepositoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void AuthenticateFailedNoStoredProcedureTest()
        {
            //// Arrange
            var errors = new List<string>();
            var authService = new AuthorizeRepository();

            //// Act
            authService.AuthenticateFailedNoStoredProcedure("user", "password", ref errors);

            //// Assert
        }
    }
}
