namespace DateBaseServices.Models 
{
    using System.Collections.Generic;

    public class Category 
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        
        public List<UserCategoryLinker> UserCategoryLinkers { get; set; }
        public List<NoteCategoryLinker> NoteCategoryLinkers { get; set; }
    }
}
