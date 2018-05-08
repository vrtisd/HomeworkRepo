using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebApp.SchoolDatabase;

namespace MyWebApp.Repository
{
    class DatabaseAccessor
    {
        private static readonly SchoolDbEntities entities;

        static DatabaseAccessor()
        {
            entities = new SchoolDbEntities();
            entities.Database.Connection.Open();
        }

        public static SchoolDbEntities Instance => entities;
    }
}
