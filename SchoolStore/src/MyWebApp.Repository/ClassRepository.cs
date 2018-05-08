using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Repository
{
    public interface IClassRepository
    {
        //ClassModel[] ForMenu(int menuId);
        ClassModel[] GetAll();
        ClassModel[] ForUser(int userId);
        void AddClass(int userId, int classId);
    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ClassRepository : IClassRepository
    {
        public void AddClass(int userId, int classId)
        {
            var @class = DatabaseAccessor.Instance.Classes.First(c => c.ClassId == classId);
            var user = DatabaseAccessor.Instance.Users.First(u => u.UserId == userId);

            user.Classes.Add(@class);
        }

        public ClassModel[] ForUser(int userId)
        {
            return DatabaseAccessor.Instance.Users
                .First(u => u.UserId == userId)
                .Classes
                .Select(c => new ClassModel
                {
                    Id = c.ClassId,
                    Name = c.ClassName,
                    Description = c.ClassDescription,
                    Price = c.ClassPrice
                }).ToArray();
        }

        public ClassModel[] GetAll()
        {
            return DatabaseAccessor.Instance.Classes
                .Select(c => new ClassModel
                {
                    Id = c.ClassId,
                    Name = c.ClassName,
                    Description = c.ClassDescription,
                    Price = c.ClassPrice
                }).ToArray();
        }
    }
}
