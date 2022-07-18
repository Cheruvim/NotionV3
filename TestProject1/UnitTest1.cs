using NUnit.Framework;

namespace TestProject1 {
    using DateBaseServices;
    using DateBaseServices.Models;

    public class Tests {
        [SetUp] public void Setup()
        {
        }

        #region UserService
        [Test]
        public void AddNewUser()
        {
            using var db = new DataContext();
            var response = db.Users.AddOrUpdate(new User
            {
                Login = "2",
                Password = "2"
            });

            Assert.True(response.Success);
        }

        [Test]
        public void UpdateUser()
        {
            using var db = new DataContext();
            var response = db.Users.AddOrUpdate(new User
            {
                UserId = 1,
                Login = "2",
                Password = "2"
            });

            Assert.True(response.Success);
        }

        [Test]
        public void GetUser()
        {
            using var db = new DataContext();
            var response = db.Users.GetUser(1);

            Assert.True(response.UserId > 0);
        }
        #endregion


    }
}