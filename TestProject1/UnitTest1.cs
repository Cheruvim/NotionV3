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
            db.Users.AddOrUpdate(new User
            {
                Login = "2",
                Password = "2"
            });
        }

        [Test]
        public void UpdateUser()
        {
            using var db = new DataContext();
            db.Users.AddOrUpdate(new User
            {
                UserId = 1,
                Login = "2",
                Password = "2"
            });
        }

        [Test]
        public void GetUser()
        {
            using var db = new DataContext();
            var response = db.Users.GetUserById(1);

            Assert.True(response.UserId > 0);
        }
        #endregion

        #region Categories

        [Test] 
        public void CreateCategoty()
        {
            using var db = new DataContext();
            db.Categories.CreateCategory(new Category
            {
                Title = "First category"
            }, 1);
        }
        
        [Test] 
        public void GetCategoty()
        {
            using var db = new DataContext();
            var cat = db.Categories.GetCategory(1, 1);
        }
        
        [Test] 
        public void UpdateCategoty()
        {
            using var db = new DataContext();
            db.Categories.UpdateCategory(new Category
            {
                CategoryId = 1,
                Title = "2nd czt"
            }, 1);
        }

        [Test] 
        public void AddUserInCategory()
        {
            using var db = new DataContext();
            db.Categories.AddUserInCategory(2,1,1);

        }

        #endregion

    }
}