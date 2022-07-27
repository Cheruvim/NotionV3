namespace TestProject1 
{
    using DateBaseServices;
    using DateBaseServices.Models;
    using NUnit.Framework;
    using SecurityService.Service;

    public class SecurityTests {
        private SecurityService _service = new();
        [SetUp] public void Setup()
        {
            
        }

        [Test]
        public void CreateToken()
        {
            var token = _service.GenerateToken(1);
            
        }

        [Test] public void ValidateToken()
        {
            CreateToken();
            var token = _service.GenerateToken(1);
            var validate = _service.ValidateCurrentToken(token);

        }

    }
}
