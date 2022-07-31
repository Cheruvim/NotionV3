using NUnit.Framework;

namespace TestProject1 {
    using DateBaseServices;
    using DateBaseServices.Models;
    using SecurityService.Service;

    public class Tests {
        private string token = SecurityService.GenerateToken(2);

        private DataContext _db;
        [SetUp] public void Setup()
        {
            _db = new DataContext();
        }

        #region UserService
        [Test]
        public void AddNewUser()
        {
            _db.Users.AddOrUpdate(new User
            {
                Login = "2",
                Password = "2"
            });
        }

        [Test]
        public void UpdateUser()
        {
            _db.Users.AddOrUpdate(new User
            {
                UserId = 2,
                Login = "2",
                Password = "2"
            }, token);
        }

        [Test]
        public void GetUser()
        {
            var response = _db.Users.GetUserById(1);

            Assert.True(response.UserId > 0);
        }
        #endregion

        #region Categories

        [Test] 
        public void CreateCategoty()
        {
            _db.Categories.CreateCategory(new Category
            {
                Title = "4th category"
            }, 1, token);
        }
        
        [Test] 
        public void GetCategoty()
        {
            var cat = _db.Categories.GetCategoryById(1, 1, token);
        }
        
        [Test] 
        public void UpdateCategoty()
        {
            _db.Categories.UpdateCategory(new Category
            {
                CategoryId = 1,
                Title = "thirth cat",
                IsDeleted = true
            }, 1, token);
        }

        [Test] 
        public void AddUserInCategory()
        {
            _db.Categories.AddUserInCategory(2,4,1, token);

        }
        
        [Test] 
        public void RemoveUserFromCategory()
        {
            _db.Categories.RemoveUserFromCategory(1,2,1, token);

        }

        [Test] public void GetAllCatByUserIdTest()
        {
            var categories = _db.Categories.GetCategoriesByUserId(1, token);
            
        }


        #endregion

        #region Security

        [Test] public void AuthorizeTest()
        {
            var currToken = _db.Users.Authorize("1", "1");
            var checkToken = SecurityService.ValidateCurrentToken(currToken.Token, currToken.UserId);

        }

        #endregion
    }
}