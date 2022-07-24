namespace NotionV3.Controllers {
    using DateBaseServices.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetMenuList : ViewComponent {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = new List<Category>
            {
                new Category{ CategoryId = 1, Title = "first cat"},
                new Category{ CategoryId = 2, Title = "Sec cat"},
            };

            return View(list);
        }

    }
}
