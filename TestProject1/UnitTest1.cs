using NUnit.Framework;

namespace TestProject1 {
    using DateBaseServices;
    using DateBaseServices.Models;

    public class Tests {
        [SetUp] public void Setup()
        {
        }

        [Test] public void Test1()
        {
            using (var db = new DataContext()){
                db.Users.Add(new User
                {
                    Login = "1",
                    Password = "1"
                });
                db.SaveChanges();
            }
        }
    }
}
