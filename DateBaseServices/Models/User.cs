namespace DateBaseServices.Models
{
    using System.Collections.Generic;
    using System.Reflection;

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public List<UserCategoryLinker> UserCategoryLinkers { get; set; }
        public List<NoteHistory> NoteHistories { get; set; }
    }
}