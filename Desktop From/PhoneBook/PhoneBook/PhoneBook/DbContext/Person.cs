using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PhoneBook.DbContext
{
    internal class Person : Connection
    {
        public static DataTable Get()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                const string sql = "SELECT * FROM Person";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    var dataT = new DataTable();
                    dataT.Load(cmd.ExecuteReader());
                    return dataT;
                }
            }
        }

        public static bool Save(string name, string mobile1, string mobile2, string mobile3, string email, string address)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var sql = string.Format("INSERT INTO Person (Name,Mobile1,Mobile2,Mobile3,Email, Address) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", name, mobile1, mobile2, mobile3, email, address);
                using (var cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


        public static bool Update(int id, string name, string mobile1, string mobile2, string mobile3, string email, string address)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var sql = string.Format("UPDATE Person SET Name = '{0}', Mobile1 ='{1}', Mobile2 = '{2}', Mobile3 = '{3}', Email = '{4}', Address = '{5}' WHERE Id = {6}", name, mobile1, mobile2, mobile3, email, address, id);
                using (var cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public static bool Delete(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var sql = string.Format("DELETE Person WHERE Id = {0}", id);
                using (var cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}
