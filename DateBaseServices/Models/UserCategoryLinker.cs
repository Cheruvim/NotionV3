namespace DateBaseServices.Models 
{
    using System.Collections.Generic;

    public class UserCategoryLinker 
    {
        public int LinkerId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int Role { get; set; }
        
        public User User { get; set; }
        public Category Category { get; set; }
        
        public List<CategoryHistory> CategoryHistories { get; set; }
    }
}
