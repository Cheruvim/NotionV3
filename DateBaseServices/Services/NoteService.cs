namespace DateBaseServices.Services 
{
    using DateBaseServices.Models;
    using Exceptions;
    using System;
    using System.Linq;

    public class NoteService : DefaultService 
    {
        internal NoteService(DataContext db) : base(db)
        {
        }

        public void AddNote(Note note, int categoryId, int userId, string token)
        {
            if (!SecurityService.Service.SecurityService.ValidateCurrentToken(token))
                throw new DbServiceException($"Токен({token}) недействителен.");

            if (note == null)
                throw new DbServiceException("Экземпляр категории не задан.");
            
            if (userId < 1)
                throw new DbServiceException("Неверный идентификатор пользователя.");
            
            if (categoryId < 1)
                throw new DbServiceException("Неверный идентификатор категории.");

            var userAccessForCategory = _db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userId && linker.CategoryId == categoryId && linker.IsDeleted == false);

            if (userAccessForCategory == null)
                throw new DbServiceException("У вас нет прав на просмотр категории с данным идентификатором.");

            note.NoteId = 0;

            var createdNote = _db.DbNotes.Add(note);
            _db.SaveChanges();
            
            _db.NoteCategoryLinkers.Add(new NoteCategoryLinker
            {
                CategoryId = categoryId,
                NoteId = createdNote.Entity.NoteId,
            });
            
            _db.NoteHistories.Add(new NoteHistory
            {
                Action = $"Создана запись с именем '{note.Title}' и идентификатором '{createdNote.Entity.NoteId}' пользователем {userId}",
                Date = DateTime.Now,
                UserCategoryLinkerId = userAccessForCategory.LinkerId
            });
            
            _db.SaveChanges();
        }

        public void EditNote(Note note, int categoryId, int userId, string token)
        {            
            if (!SecurityService.Service.SecurityService.ValidateCurrentToken(token))
                throw new DbServiceException($"Токен({token}) недействителен.");

            if (note == null)
                throw new DbServiceException("Экземпляр категории не задан.");
            
            if (userId < 1)
                throw new DbServiceException("Неверный идентификатор пользователя.");
            
            if (categoryId < 1)
                throw new DbServiceException("Неверный идентификатор категории.");

            var linkerNoteCat = _db.NoteCategoryLinkers.FirstOrDefault(linker => linker.NoteId == note.NoteId && linker.CategoryId == categoryId);
            if (linkerNoteCat == null)
                throw new DbServiceException("Запись не пренадлежит к данной категории");

            var userAccessForCategory = _db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userId && linker.CategoryId == categoryId && linker.IsDeleted == false);
            if (userAccessForCategory == null)
                throw new DbServiceException("У вас нет прав на просмотр категории с данным идентификатором.");

            var currentNote = _db.DbNotes.FirstOrDefault(n => n.NoteId == note.NoteId);
            if (currentNote == null || currentNote.IsDeleted)
                throw new DbServiceException("Не удалось найти запись с данным идентификатором для обновления.");
            
            currentNote.Title = note.Title;
            currentNote.Header = note.Header;
            currentNote.Body = note.Body;
            
            var createdNote = _db.DbNotes.Update(currentNote);
            
            _db.NoteHistories.Add(new NoteHistory
            {
                Action = $"Создана запись с именем '{note.Title}' и идентификатором '{createdNote.Entity.NoteId}' пользователем {userId}",
                Date = DateTime.Now,
                UserCategoryLinkerId = userAccessForCategory.LinkerId
            });
            
            _db.SaveChanges();
        }
    }
}
