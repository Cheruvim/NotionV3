using System.Threading.Tasks;
using DateBaseServices;
using Microsoft.AspNetCore.Mvc;
using NotionV3.Utils;

namespace NotionV3.Components 
{
    public class GetAuthorizedUserViewComponent : ViewComponent
    {
        private DataContext _db = new DataContext();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = UserCookieUtility.GetUserInfoFromCookies(HttpContext);
            return View(currentUser);
        }
    }
}
