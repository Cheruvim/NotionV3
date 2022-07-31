namespace NotionV3.Controllers 
{
    using DateBaseServices;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Utils;

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
