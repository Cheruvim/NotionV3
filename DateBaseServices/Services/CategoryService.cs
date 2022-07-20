namespace DateBaseServices.Services
{
    using DateBaseServices.Models;
    using Exceptions;
    using System;
    using System.Linq;

    public class CategoryService : DefaultService
    {
        public CategoryService(DataContext db) : base(db)
        {
        }

        public void CreateCategory(Category category, int userId)
        {
            if (category == null)
                throw new DbServiceException("Экземпляр категории не задан.");
            
            if (string.IsNullOrEmpty(category.Title))
                throw new DbServiceException("Не заданно имя категории.");

            if (userId < 1)
                throw new DbServiceException("Неверный идентификатор пользователя.");

            var createdCat = _db.DbCategories.Add(category);
            var createdLinker = _db.UserCategoryLinkers.Add(new UserCategoryLinker
            {
                Category = category,
                UserId = userId,
                Role = 99
            });

            _db.SaveChanges();
            _db.CategoryHistories.Add(new CategoryHistory
            {
                Action = $"Создана категория с именем '{category.Title}' и идентификатором '{createdCat.Entity.CategoryId}'",
                Date = DateTime.Now,
                UserCategoryLinkerId = createdLinker.Entity.LinkerId,
            });
            
            _db.SaveChanges();
        }
        
        public void UpdateCategory(Category category, int userId)
        {
            if (category == null)
                throw new DbServiceException("Экземпляр категории не задан.");
            
            if (string.IsNullOrEmpty(category.Title))
                throw new DbServiceException("Не заданно имя категории.");
            
            if (category.CategoryId < 1)
                throw new DbServiceException("Не задан идентификатор категории.");

            var currentCategory = GetCategory(category.CategoryId, userId);
            var pastTitle = currentCategory.Title;
            currentCategory.Title = category.Title;
            _db.DbCategories.Update(currentCategory);
            
            _db.SaveChanges();

            var linker = _db.UserCategoryLinkers
                .FirstOrDefault(linker => linker.CategoryId == currentCategory.CategoryId && linker.UserId == userId);

            if (linker == null)
                throw new DbServiceException("Пользователя с данным идентификатором нет в данной категории.");
            
            _db.CategoryHistories.Add(new CategoryHistory
            {
                Action = $"Имя для категории с идентификатором '{currentCategory.CategoryId}' изменено с '{pastTitle}' на '{currentCategory.Title}'.",
                Date = DateTime.Now,
                UserCategoryLinkerId = linker.LinkerId,
            });
            
            _db.SaveChanges();
        }

        public Category GetCategory(int categoryId, int userId)
        {
            if (categoryId < 1)
                throw new DbServiceException("Указан неверный идентификатор категории.");

            var currentCategory = _db.DbCategories.FirstOrDefault(cat => cat.CategoryId == categoryId);
            if (currentCategory == null)
                throw new DbServiceException("Не удалось найти категорию с данным идентификатором.");


            if (_db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userId && linker.CategoryId == categoryId) == null)
                throw new DbServiceException("У вас нет прав на просмотр категории с данным идентификатором.");

            return currentCategory;
        }

        public void AddUserInCategory(int userId, int categoryId, int userActionStartId)
        {
            if (categoryId < 1)
                throw new DbServiceException("Указан неверный идентификатор категории.");

            if (userId < 1)
                throw new DbServiceException("Указан неверный идентификатор пользователя добавляемого.");
            
            if (userActionStartId < 1)
                throw new DbServiceException("Указан неверный идентификатор пользователя создателя запроса.");
            
            if (_db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userActionStartId && linker.CategoryId == categoryId) == null)
                throw new DbServiceException("У вас нет прав на просмотр категории с данным идентификатором.");

            GetCategory(categoryId, userActionStartId);
            _db.Users.GetUserById(userActionStartId);
            _db.Users.GetUserById(userId);

            _db.UserCategoryLinkers.Add(new UserCategoryLinker
            {
                CategoryId = categoryId,
                UserId = userId
            });
            _db.SaveChanges();
        }
    }
}