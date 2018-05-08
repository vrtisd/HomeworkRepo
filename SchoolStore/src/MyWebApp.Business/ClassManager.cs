using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebApp.Repository;

namespace MyWebApp.Business
{
    public interface IClassManager
    {
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

    class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public void AddClass(int userId, int classId)
        {
            classRepository.AddClass(userId, classId);
        }

        public ClassModel[] ForUser(int userId)
        {
            return classRepository.ForUser(userId)
                .Select(c => new ClassModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price
                }).ToArray();
        }

        public ClassModel[] GetAll()
        {
            return classRepository.GetAll()
                .Select(c => new ClassModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price
                }).ToArray();
        }
    }
}
