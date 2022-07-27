namespace NotionV3.Controllers {
    using DateBaseServices;
    using DateBaseServices.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetCategoriesViewComponent : ViewComponent
    {
        GetCategoriesViewComponent(DataContext db)
        {
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = new List<Category>
            {
                new Category{CategoryId = 1, Title = "First"},
                new Category{CategoryId = 2, Title = "Secound"},

            };
            return View(list);
        }
    }

}
