using System;
using System.Linq;
using DateBaseServices.Exceptions;
using DateBaseServices.Models;
using DateBaseServices.Services.Models;

namespace DateBaseServices.Services
{
    public class UserService : DefaultService
    {
        public UserService(DataContext db) : base(db)
        {
        }

        public void AddOrUpdate(User user)
        {
            if (user.UserId < 1)
            {
                AddUser(user);
            }
            else
            {
                UpdateUser(user);
            }
        }

        private void AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Password))
                throw new DbServiceException("Введите пароль.");

            if (_db.DbUsers.FirstOrDefault(u => u.Login == user.Login) != null)
                throw new DbServiceException("Пользователь с данным логином уже существует.");

            _db.DbUsers.Add(user);
            _db.SaveChanges();
        }

        private void UpdateUser(User user)
        {
            var currentUser = _db.DbUsers.FirstOrDefault(u => u.UserId == user.UserId && u.Login == user.Login);

            if (currentUser == null)
                throw new DbServiceException("Пользователя с данным id не существует.");

            if (!string.IsNullOrEmpty(user.Password))
                currentUser.Password = user.Password;

            if (!string.IsNullOrEmpty(user.Name))
                currentUser.Name = user.Name;

            if (!string.IsNullOrEmpty(user.Description))
                currentUser.Description = user.Description;

            _db.DbUsers.Update(currentUser);
            _db.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            if (userId < 1)
                throw new DbServiceException("Указан некорректный идентификатор пользователя.");

            var user = _db.DbUsers.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                throw new DbServiceException("Не удалось найти пользователя с данным идентификатором.");

            user.Password = null;
            return user;
        }
    }
}