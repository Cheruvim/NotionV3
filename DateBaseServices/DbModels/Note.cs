﻿using System.Collections.Generic;

namespace DateBaseServices.DbModels
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public bool IsDeleted { get; set; }

        public List<NoteCategoryLinker> NoteCategoryLinkers { get; set; }
        public List<NoteHistory> NoteHistories { get; set; }
    }
}