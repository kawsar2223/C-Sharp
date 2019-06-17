using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp
{
    class Database
    {
        private static List<UserModel> UserTable = new List<UserModel>();
        public static List<UserModel> SelectAll()
        {
            return Database.UserTable;
        }
        public static UserModel Select(int userId)
        {

            var user = new UserModel();
            foreach (UserModel row in Database.UserTable)
            {
                if (row.UserId == userId)
                    user = row;
            }

            return user;

        }
        public static void Insert(UserModel row)
        {
            Database.UserTable.Add(row);
        }
        public static void Update(UserModel row)
        {
            var user = new UserModel();

            for (var i = 0; i < Database.UserTable.Count; i++)
            {
                var ci = Database.UserTable[i];
                //if (ci.UserId == row.UserId)
                //{
                //    Database.UserTable.Remove(ci);
                //    Database.UserTable.Add(row);
                //}

                if (ci.UserId == row.UserId)
                {
                    ci.Email = row.Email;
                    ci.FirstName = row.FirstName;
                }
            }
        }

        public void Delete(UserModel row)
        {
            Database.UserTable.Remove(row);
        }
    }
}
