using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var row = new UserModel();
            row.UserId = 1;
            row.Username = "admin";
            row.FirstName = "Admin";
            row.LastName = "User";
            row.Email = "admin@site.com";

            Database.Insert(row);
            Database.Insert(row);
            Database.Insert(row);

            var users = Database.SelectAll();
            foreach(UserModel u in users)
               Console.WriteLine(u.Username + " " + u.Email);

            
            Console.ReadLine();

           Console.Write("Do you want to insert record?");
            var input = Console.ReadLine();
            if (input.Equals("1"))
            {
                Database.Insert(new UserModel
                {
                    UserId = 2, Username = "demo1", FirstName = "", LastName = string.Empty, Email = "Test@site.com"
                });
                var users2 = Database.SelectAll();
                foreach (UserModel u in users2)
                    Console.WriteLine(u.Username + " " + u.Email);

                Console.ReadLine();
            }


        }

      
    }
}
