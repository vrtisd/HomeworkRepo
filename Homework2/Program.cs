using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<Models.User>
            {
                new Models.User { Name = "Dave", Password = "hello" },
                new Models.User { Name = "Steve", Password = "steve" },
                new Models.User { Name = "Lisa", Password = "hello" }
            };

            var helloPasswords = users.Where(u => u.Password == "hello").GetEnumerator();
            
            while(helloPasswords.MoveNext())
            {
                Console.WriteLine($"Name: {helloPasswords.Current.Name} | Password: {helloPasswords.Current.Password}");
            }

            Console.WriteLine("-----------------------------");

            users.RemoveAll(u => u.Name.ToLower() == u.Password);

            users.Remove(users.FirstOrDefault(u => u.Password == "hello"));

            var remainingUsers = users.GetEnumerator();

            while (remainingUsers.MoveNext())
            {
                Console.WriteLine($"Name: {remainingUsers.Current.Name} | Password: {remainingUsers.Current.Password}");
            }

            Console.WriteLine("-----------------------------");
        }
    }
}
