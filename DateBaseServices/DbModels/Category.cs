using System.Collections.Generic;

namespace DateBaseServices.DbModels 
{
    public class Category 
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        
        public List<UserCategoryLinker> UserCategoryLinkers { get; set; }
        public List<NoteCategoryLinker> NoteCategoryLinkers { get; set; }
    }
}
