using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
//using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using T8_20230629;

namespace T10_20230704
{
    internal class DbConnector
    {
        public string Server { get; set; }
        public string  Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string EmployeeTb { get; set; }
        public CryptoGraphy CryptoGraphy = new();

        public DbConnector()
        {
            this.Server = "CNTT-TRUNGNV-PC";
            this.Database = "MANAGER";
            this.User = "sa";
            this.Password = "@Automation1";
            this.EmployeeTb = "EMPLOYEES";
        }

        private string BuildConnectionString()
        {
            return String.Format("Server={0};Database={1};User Id={2};Password={3};",Server,Database, User, Password);
        }

        private bool CheckDbExist()
        {
            string connMasterString = "Server=CNTT-TRUNGNV-PC;Database=master;User Id=sa;Password=@Automation1";
            var cmdCheck = "select count(*) from master.dbo.sysdatabases where name = @databases";
            using (SqlConnection conn = new SqlConnection(connMasterString))
            {
                using (SqlCommand sqlCmd = new SqlCommand(cmdCheck, conn))
                {
                    sqlCmd.Parameters.AddWithValue("databases", this.Database);
                    conn.Open();                    
                    return Convert.ToBoolean(sqlCmd.ExecuteScalar());
                }
            }
        }
        public SqlConnection GetConnection()
        {
            string connString = this.BuildConnectionString();
            string connMasterString = "Server=CNTT-TRUNGNV-PC;Database=master;User Id=sa;Password=@Automation1";
            string cmdString = "CREATE DATABASE "+this.Database;
            bool checkExist = this.CheckDbExist();
            if (!checkExist)
            {
                using (SqlConnection connection = new SqlConnection(connMasterString)) 
                {
                    SqlCommand sqlCmd = new SqlCommand(cmdString, connection);                
                    connection.Open();
                    sqlCmd.ExecuteNonQuery();
                }
            }
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
     
        public void InsertTable(string table, Employee EmpEnter)
        {
            SqlConnection conn = this.GetConnection();
            conn.Open();
            SqlCommand cmd, cmd2;
            
            try
            {
                string sql = "INSERT INTO " + table + "(no,name,email,password,ismanager) VALUES (@0,@1,@2,@3,@4)";
                using (cmd2 = new SqlCommand(sql, conn))
                {
                    cmd2.Parameters.AddWithValue("0", EmpEnter.no);
                    cmd2.Parameters.AddWithValue("1", EmpEnter.name);
                    cmd2.Parameters.AddWithValue("2", EmpEnter.email);
                    cmd2.Parameters.AddWithValue("3", CryptoGraphy.Hash(EmpEnter.password, "SHA512"));
                    cmd2.Parameters.AddWithValue("4", EmpEnter.isManager);
                    cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    string cmdString = "CREATE TABLE " + table + " (no varchar(50),name varchar(50), email varchar(50), password varchar(MAX), ismanager bit )";
                    using (cmd = new SqlCommand(cmdString, conn))
                    {
                        cmd.Parameters.AddWithValue("table", table);
                        cmd.ExecuteNonQuery();
                    }
                    string sql = "INSERT INTO " + table + "(no,name,email,password,ismanager) VALUES (@0,@1,@2,@3,@4)";
                    using (cmd2 = new SqlCommand(sql, conn))
                    {
                        cmd2.Parameters.AddWithValue("0", EmpEnter.no);
                        cmd2.Parameters.AddWithValue("1", EmpEnter.name);
                        cmd2.Parameters.AddWithValue("2", EmpEnter.email);
                        cmd2.Parameters.AddWithValue("3", CryptoGraphy.Hash(EmpEnter.password, "SHA512"));
                        cmd2.Parameters.AddWithValue("4", EmpEnter.isManager);
                        cmd2.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                
            }                      
            conn.Close();
        }
        public void Find(string tableName, string field, string searchString)
        {
            SqlConnection conn = this.GetConnection();
            conn.Open();
            //string sql = "select no, name, email from EMPLOYEES where name = @searchString";
            string sql = $"select no,name,email from {tableName} where {field} = @searchString ";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("searchString", searchString);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("no, name, email");
                        while (reader.Read())
                        {
                            Console.WriteLine("{0},{1},{2}", reader[0], reader[1], reader[2]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not Found");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }        
            conn.Close();           
        }
        public string FindIndex(string tableName, string field, string searchString)
        {
            string index = "";
            SqlConnection conn = this.GetConnection();
            conn.Open();            
            string sql = $"select no,name from {tableName} where {field} = @searchString ";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {                    
                    cmd.Parameters.AddWithValue("searchString", searchString);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            index = string.Format($"{reader[0]}");
                        }                        
                    }
                    else
                    {
                        Console.WriteLine("Not Found");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            conn.Close();
            return index;
        }

        public void Update(string tableName, string field, string updateString, string index)
        {
            SqlConnection conn = this.GetConnection();
            conn.Open();
            try
            {
                string sql = $"UPDATE {tableName} SET {field} = @updateString WHERE no = @index";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("updateString", updateString);
                    cmd.Parameters.AddWithValue("index", index);
                    cmd.ExecuteNonQuery();
                }
            }
            catch( Exception e )
            {
                Console.WriteLine(e.Message);
            }            
        }

        public void Delete(string tableName, string index)
        {
            SqlConnection conn = this.GetConnection();
            conn.Open();
            try
            {
                string sql = $"DELETE FROM {tableName} WHERE NO = @index ";
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("index", index); 
                    cmd.ExecuteNonQuery();
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine(e.Message);
            }
            conn.Close ();
        }

        public void View(string tableName)
        {
            SqlConnection conn = this.GetConnection();
            conn.Open();
            string sql = $"SELECT NO,NAME,EMAIL FROM {tableName}";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("no, name, email");
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}, {1}, {2}", reader["no"], reader["name"], reader["email"]);
                    }
                }
                else
                {
                    Console.WriteLine(" NO THING ");
                }
            }          
        }

        public SqlDataReader Reader(string tableName)
        {
            SqlDataReader reader = null;
            SqlConnection conn = this.GetConnection();
            conn.Open();
            string sql = $" SELECT * FROM {tableName}";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    reader = cmd.ExecuteReader();
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine(e.Message);   
            }                      
            return reader;
        }
        public int CheckLogin(string email, string password)
        {
            int check = 0 ;
            using (SqlConnection conn = this.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM EMPLOYEES WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD";
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("EMAIL", email);
                    
                    cmd.Parameters.AddWithValue("PASSWORD", CryptoGraphy.Hash(password, "SHA512"));
                    bool result = Convert.ToBoolean(cmd.ExecuteScalar());
                    if(result)
                    {
                        string sql2 = "SELECT EMAIL, PASSWORD, ISMANAGER FROM EMPLOYEES WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD";
                        using (SqlCommand cmd2 = new SqlCommand(sql2, conn))
                        {
                            cmd2.Parameters.AddWithValue("EMAIL", email);
                            cmd2.Parameters.AddWithValue("PASSWORD", CryptoGraphy.Hash(password, "SHA512"));
                            using(SqlDataReader reader = cmd2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while(reader.Read()) 
                                    {
                                        bool isManagerCheck = Convert.ToBoolean(reader[2]);
                                        if (isManagerCheck)
                                        {
                                            check = 1;
                                        }
                                        else
                                        {
                                            check = 2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {                       
                        
                        check = 3;                        
                    }
                }
            }
            return check;
        }
        
    }
    
}
