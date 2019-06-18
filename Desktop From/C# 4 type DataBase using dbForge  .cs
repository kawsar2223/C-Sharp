using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using PhoneBook.Models;

namespace PhoneBook.DbContext
{
    internal class Person : Connection
    {
		
		//ADO.NET using
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
				
				//ADO.NET using Parameters
				
        public static DataTable Get(int personId)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var sql = "SELECT * FROM Person WHERE Id =" + personId;
                using (var cmd = new SqlCommand(sql, conn))
                {
                    var dataT = new DataTable();
                    dataT.Load(cmd.ExecuteReader());
                    return dataT;
                }
            }
        }
					
					//StoredProcedure using 
        public static DataTable GetWithSp()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                const string sql = "usp_Person_Get";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dataT = new DataTable();
                    dataT.Load(cmd.ExecuteReader());
                    return dataT;
                }
            }
        }
		
						//StoredProcedure using Parameters
						
        public static DataTable GetWithSp(int personId)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                const string sql = "usp_Person_Get";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonId", personId);
                    var dataT = new DataTable();
                    dataT.Load(cmd.ExecuteReader());
                    return dataT;
                }
            }
        }
						//StoredProcedure using Dapper
        public static DataTable GetWithSpDapper(int personId)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {

                var dr = conn.ExecuteReader("usp_Person_Get", new { PersonId = personId }, commandType: CommandType.StoredProcedure);
                var dataT = new DataTable();
                dataT.Load(dr);
                return dataT;
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


        public static bool Save(PersonModel person)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var sql = string.Format("INSERT INTO Person (Name,Mobile1,Mobile2,Mobile3,Email, Address) VALUES (@Name, @Mobile1, @Mobile2, @Mobile3, @Email, @Address)");
                using (var cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@Name", person.Name);
                        cmd.Parameters.AddWithValue("@Mobile1", person.Mobile1);
                        cmd.Parameters.AddWithValue("@Mobile2", person.Mobile2);
                        cmd.Parameters.AddWithValue("@Mobile3", person.Mobile3);
                        cmd.Parameters.AddWithValue("@Email", person.Email);
                        cmd.Parameters.AddWithValue("@Address", person.Address);
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
        public static bool SaveDapper(PersonModel person)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {

                var param = new
                {
                    person.Mobile1,
                    person.Mobile2,
                    person.Mobile3,
                    person.Email,
                    person.Address,

                };
                var r = conn.Execute("usp_Person_Save", param, commandType: CommandType.StoredProcedure);
                return r > 0;
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
