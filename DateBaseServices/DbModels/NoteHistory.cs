namespace DateBaseServices.Models 
{
    using System;

    public class NoteHistory 
    {
        public int HistoryId { get; set; }
        public int UserCategoryLinkerId { get; set; }
        public int NoteId { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        
        public UserCategoryLinker UserCategoryLinker { get; set; }
        public Note Note { get; set; }
    }
}
