using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Repository
{
    public interface IUserRepository
    {
        UserModel[] Users { get; }
        UserModel User(int userId);

        //Adding code for LogIn and Register
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

    class UserRepository : IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(u => u.UserEmail.ToLower() == email.ToLower()
                && u.UserPassword == password);

            if (user == null)
                return null;

            //return new UserModel { Id = user.UserId, Name = user.UserName, Email = user.UserEmail, Password=user.UserPassword, IsAdmin=user.UserIsAdmin };
            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        //public UserModel Register(string name, string email, string password)
        public UserModel Register(string email, string password)
        {
            //Check to see whether the UserEmail already exists
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(u => u.UserEmail.ToLower() == email.ToLower());

            if (user != null)
                return null;
            else
            {
                //var user = DatabaseAccessor.Instance.Users
                //    .Add(new MyWebApp.SchoolDatabase.User { UserName = name, UserEmail = email, UserPassword = password });
                user = DatabaseAccessor.Instance.Users
                    .Add(new MyWebApp.SchoolDatabase.User { UserName = email, UserEmail = email, UserPassword = password, UserIsAdmin = false });

                DatabaseAccessor.Instance.SaveChanges();

                //return new UserModel { Id = user.UserId, Name = user.UserName, Email = user.UserEmail, Password=user.UserPassword, IsAdmin=user.UserIsAdmin };
                return new UserModel { Id = user.UserId, Name = user.UserEmail };
            }
        }

        public UserModel[] Users
        {
            get
            {
                return DatabaseAccessor.Instance.Users
                    .Select(u => new UserModel
                    {
                        Id = u.UserId,
                        Name = u.UserName,
                        Email = u.UserEmail,
                        Password = u.UserPassword,
                        IsAdmin = u.UserIsAdmin
                    }).ToArray();
            }
        }

        public UserModel User(int userId)
        {
            var user = DatabaseAccessor.Instance.Users
                .Where(u => u.UserId == userId)
                .Select(u => new UserModel
                {
                    Id = u.UserId,
                    Name = u.UserName,
                    Email = u.UserEmail,
                    Password = u.UserPassword,
                    IsAdmin = u.UserIsAdmin
                }).First();

            return user;
        }
    }
}
