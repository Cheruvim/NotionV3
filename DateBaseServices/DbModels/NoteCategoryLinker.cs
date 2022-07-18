namespace DateBaseServices.Models 
{

    public class NoteCategoryLinker 
    {
        public int LinkerId { get; set; }
        public int CategoryId { get; set; }
        public int NoteId { get; set; }
        
        public Category Category { get; set; }
        public Note Note { get; set; }
    }
}
