namespace TestProject1 
{
    using DateBaseServices;
    using DateBaseServices.Models;
    using NUnit.Framework;
    using SecurityService.Service;

    public class SecurityTests {
        [SetUp] public void Setup()
        {
            
        }

        [Test]
        public void CreateToken()
        {
            var token = SecurityService.GenerateToken(1);
            
        }

        [Test] public void ValidateToken()
        {
            CreateToken();
            var token = SecurityService.GenerateToken(1);
            var validate = SecurityService.ValidateCurrentToken(token, 1);

        }

    }
}
