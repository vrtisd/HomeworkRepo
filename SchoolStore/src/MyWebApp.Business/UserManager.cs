using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebApp.Repository;

namespace MyWebApp.Business
{
    public interface IUserManager
    {
        UserModel[] Users { get; }
        UserModel User(int userId);

        UserModel LogIn(string email, string password);
        //UserModel Register(string name, string email, string password);
        UserModel Register(string email, string password);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public UserModel() { }

        public UserModel(int id, string name, string email, string password, bool isAdmin)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }
    }

    class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel LogIn(string email, string password)
        {
            var user = userRepository.LogIn(email, password);

            if (user == null)
                return null;

            //return new UserModel { Id = user.Id, Name = user.Name, Email = user.Email, Password = user.Password, IsAdmin = false };
            return new UserModel { Id = user.Id, Name = user.Name };
        }

        //public UserModel Register(string name, string email, string password)
        public UserModel Register(string email, string password)
        {
            //var user = userRepository.Register(name, email, password);
            var user = userRepository.Register(email, password);

            if (user == null)
                return null;

            //return new UserModel { Id = user.Id, Name = user.Name, Email = user.Email, Password = user.Password, IsAdmin = user.IsAdmin };
            return new UserModel { Id = user.Id, Name = user.Name };
        }

        public UserModel[] Users
        {
            get
            {
                return userRepository.Users
                    .Select(u => new UserModel(u.Id, u.Name, u.Email, u.Password, u.IsAdmin))
                    .ToArray();
            }
        }

        public UserModel User(int userId)
        {
            var userModel = userRepository.User(userId);
            return new UserModel(userModel.Id, userModel.Name, userModel.Email, userModel.Password, userModel.IsAdmin);
        }
    }
}
