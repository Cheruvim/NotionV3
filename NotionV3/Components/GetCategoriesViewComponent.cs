using System.Collections.Generic;
using System.Threading.Tasks;
using DateBaseServices;
using DateBaseServices.DbModels;
using Microsoft.AspNetCore.Mvc;
using NotionV3.Utils;

namespace NotionV3.Components {
    public class GetCategoriesViewComponent : ViewComponent
    {
        private DataContext _db = new DataContext();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var catList = new List<Category>();
            var currentUser = UserCookieUtility.GetUserInfoFromCookies(HttpContext);
            if(currentUser != null && !string.IsNullOrEmpty(currentUser.Login) && currentUser.UserId != 0 && !string.IsNullOrEmpty(currentUser.Token))
                catList = _db.Categories.GetCategoriesByUserId(currentUser.UserId, currentUser.Token);

            return View(catList);
        }
    }

}