namespace DateBaseServices.Services
{
    using DateBaseServices.Models;
    using Exceptions;
    using System;
    using System.Linq;

    public class CategoryService : DefaultService
    {
        internal CategoryService(DataContext db) : base(db)
        {
        }

        public void CreateCategory(Category category, int userId, string token)
        {
            if (!SecurityService.Service.SecurityService.ValidateCurrentToken(token))
                throw new DbServiceException($"Токен({token}) недействителен.");

            if (category == null)
                throw new DbServiceException("Экземпляр категории не задан.");
            
            if (string.IsNullOrEmpty(category.Title))
                throw new DbServiceException("Не заданно имя категории.");

            if (userId < 1)
                throw new DbServiceException("Неверный идентификатор пользователя.");

            var createdCat = _db.DbCategories.Add(category);
            _db.SaveChanges();
            
            var createdLinker = _db.UserCategoryLinkers.Add(new UserCategoryLinker
            {
                CategoryId = createdCat.Entity.CategoryId,
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
        
        public void UpdateCategory(Category category, int userId, string token)
        {
            if (!SecurityService.Service.SecurityService.ValidateCurrentToken(token))
                throw new DbServiceException($"Токен({token}) недействителен.");

            if (category == null)
                throw new DbServiceException("Экземпляр категории не задан.");
            
            if (string.IsNullOrEmpty(category.Title))
                throw new DbServiceException("Не заданно имя категории.");
            
            if (category.CategoryId < 1)
                throw new DbServiceException("Не задан идентификатор категории.");

            var currentCategory = GetCategory(category.CategoryId, userId, token);
            var pastTitle = currentCategory.Title;
            currentCategory.Title = category.Title;
            _db.DbCategories.Update(currentCategory);
            
            _db.SaveChanges();

            var linker = _db.UserCategoryLinkers
                .FirstOrDefault(linker => linker.CategoryId == currentCategory.CategoryId && linker.UserId == userId && linker.IsDeleted == false);

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

        public Category GetCategory(int categoryId, int userId, string token)
        {
            if (!SecurityService.Service.SecurityService.ValidateCurrentToken(token))
                throw new DbServiceException($"Токен({token}) недействителен.");

            if (categoryId < 1)
                throw new DbServiceException("Указан неверный идентификатор категории.");

            var currentCategory = _db.DbCategories.FirstOrDefault(cat => cat.CategoryId == categoryId && cat.IsDeleted == false);
            if (currentCategory == null)
                throw new DbServiceException("Не удалось найти категорию с данным идентификатором.");
            
            if (_db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userId && linker.CategoryId == categoryId && linker.IsDeleted == false) == null)
                throw new DbServiceException("У вас нет прав на просмотр категории с данным идентификатором.");

            return currentCategory;
        }

        public void AddUserInCategory(int userId, int categoryId, int userActionStartId, string token, int role = 0)
        {
            if (!SecurityService.Service.SecurityService.ValidateCurrentToken(token))
                throw new DbServiceException($"Токен({token}) недействителен.");

            if (categoryId < 1)
                throw new DbServiceException("Указан неверный идентификатор категории.");

            if (userId < 1)
                throw new DbServiceException("Указан неверный идентификатор пользователя добавляемого.");
            
            if (userActionStartId < 1)
                throw new DbServiceException("Указан неверный идентификатор пользователя создателя запроса.");

            var userLinker = _db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userActionStartId && linker.CategoryId == categoryId && linker.IsDeleted == false);
            if (userLinker == null)
                throw new DbServiceException("У вас нет прав на просмотр категории с данным идентификатором.");

            if (userLinker.Role < role)
                throw new DbServiceException("Вы не можете добавить пользователя с правами выше ваших.");

            var cat = GetCategory(categoryId, userActionStartId, token);
            _db.Users.GetUserById(userActionStartId, token);
            _db.Users.GetUserById(userId, token);

            var linker = _db.UserCategoryLinkers.Add(new UserCategoryLinker
            {
                CategoryId = categoryId,
                UserId = userId,
                Role = role
            });
            _db.SaveChanges();
            
            _db.CategoryHistories.Add(new CategoryHistory
            {
                Action = $"В категорию с идентификатором {cat.CategoryId} добавлен пользователь с идентификатором {userId} пользователем с идентификатором {userActionStartId}",
                Date = DateTime.Now,
                UserCategoryLinkerId = linker.Entity.LinkerId,
            });

        }
        
        public void RemoveUserFromCategory(int userId, int categoryId, int userActionStartId, string token)
        {
            if (!SecurityService.Service.SecurityService.ValidateCurrentToken(token))
                throw new DbServiceException($"Токен({token}) недействителен.");

            if (categoryId < 1)
                throw new DbServiceException("Указан неверный идентификатор категории.");

            if (userId < 1)
                throw new DbServiceException("Указан неверный идентификатор пользователя добавляемого.");
            
            if (userActionStartId < 1)
                throw new DbServiceException("Указан неверный идентификатор пользователя создателя запроса.");

            var userRemover = _db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userActionStartId && linker.CategoryId == categoryId && linker.IsDeleted == false);
            var userForRemove = _db.UserCategoryLinkers.FirstOrDefault(linker => linker.UserId == userId && linker.CategoryId == categoryId && linker.IsDeleted == false);

            if (userRemover == null)
                throw new DbServiceException("У вас нет прав на взаимодествие с категорией с данным идентификатором.");

            if (userForRemove == null)
                throw new DbServiceException("Удаляемого пользователя нет в данной категории.");

            if (userForRemove.Role > userRemover.Role)
                throw new DbServiceException("Вы не можете удалить пользователя с правами выше ваших.");

            var cat =  GetCategory(categoryId, userActionStartId, token);
            
            //// для проверки на наличие пользователей.
            _db.Users.GetUserById(userActionStartId, token);
            _db.Users.GetUserById(userId, token);

            userForRemove.IsDeleted = true;
            _db.UserCategoryLinkers.Update(userForRemove);
            _db.SaveChanges();
            
            _db.CategoryHistories.Add(new CategoryHistory
            {
                Action = $"Из категории с идентификатором {cat.CategoryId} был удален пользователь с идентификатором {userId} пользователем с идентификатором {userActionStartId}",
                Date = DateTime.Now,
                UserCategoryLinkerId = userForRemove.LinkerId,
            });

        }
    }
}