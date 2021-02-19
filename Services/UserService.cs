using Depot;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly DataBase _dataBase;

        public UserService()
        {
            _dataBase = new DataBase();
        }
        public void RegisterUser(User user)
        {
            User account = new User(user);
            _dataBase.Add(user);
            _dataBase.SaveChanges();
        }

        public User LoginUser(LogInModel user)
        {
            return _dataBase.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
        }
        public User GetUserByEmail(string email)
        {
            return _dataBase.Users.FirstOrDefault(u =>
            u.Email == email);
        }

        public User ReturnUser(string email)
        {
            return _dataBase.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}

