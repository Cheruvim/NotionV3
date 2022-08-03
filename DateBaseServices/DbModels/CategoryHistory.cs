using System;

namespace DateBaseServices.DbModels 
{
    public class CategoryHistory 
    {
        public int HistoryId { get; set; }
        public int UserCategoryLinkerId { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        
        public UserCategoryLinker UserCategoryLinker { get; set; }
    }
}
