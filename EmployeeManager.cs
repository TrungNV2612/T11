using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
//using System.Net.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
//using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace T10_20230704
{
    internal class EmployeeManager:BaseManager
    {                       
        internal string exportPath = "E:\\SynologyDrive\\Drive\\0. CNTT\\0.FULL STACK\\2. Bai tap C#\\T10_20230704\\T10ExportList.csv";
        internal DbConnector connector = new DbConnector();
        internal EmployeeManager(string name) : base(name)
        {
            Employee admin = new Employee("0","admin","admin@gmail.com","admin",true);
            string index = this.connector.FindIndex(connector.EmployeeTb, "name", "admin");
            if(index == "")
            {
                this.connector.InsertTable(connector.EmployeeTb, admin);
            }                        
        }
        internal override void Find()
        {
            Console.WriteLine("Please enter field which to find: no, name or email ");
            string fieldFind = Console.ReadLine();
            Console.WriteLine("Please enter string which to find: ");
            string searchString = Console.ReadLine();            
            connector.Find(connector.EmployeeTb, fieldFind, searchString);
        }
        
        internal override void AddNew()
        {            
            Employee EmpEnter = EnterEmployee();            
            SqlConnection conn = connector.GetConnection();            
            connector.InsertTable(connector.EmployeeTb, EmpEnter);            
        }
        internal override void Update()
        {
            Console.WriteLine("Please enter field which to find: no or name ");
            string field = Console.ReadLine();
            Console.WriteLine("Please enter string which to find: ");
            string searchString = Console.ReadLine() ;
            string index = connector.FindIndex(connector.EmployeeTb, field, searchString);
            Console.WriteLine("Please enter field which to update: name, email, password or ismanager ");
            string fieldUpdate = Console.ReadLine();
            Console.WriteLine("Please enter string which to update: ");
            string updateString = Console.ReadLine();
            connector.Update(connector.EmployeeTb, fieldUpdate, updateString, index);                  

        }
        internal override void Delete()
        {
            Console.WriteLine("Please enter field which to find: no or name ");
            string field = Console.ReadLine();
            Console.WriteLine("Please enter string which to find: ");
            string searchString = Console.ReadLine();
            string index = connector.FindIndex(connector.EmployeeTb, field, searchString);
            this.connector.Delete(connector.EmployeeTb, index);
        }
        internal override void ViewAll()
        {
            connector.View(connector.EmployeeTb);
        }
        internal override void Export()
        {            
            try
            {
                using (StreamWriter writer = new StreamWriter(exportPath))
                {                    
                    using (SqlConnection conn = connector.GetConnection())
                    {
                        using (SqlDataReader reader = connector.Reader(connector.EmployeeTb))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    writer.WriteLine("{0},{1},{2},{3},{4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                                }
                            }
                            else
                            {
                                Console.Write("NO THING");
                            }
                        }                                                 
                    }
                }                                            
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }                                
        }
        internal override void Import()
        {
            Console.WriteLine("Please enter import path: ");
            string importPath = Console.ReadLine();
            using (StreamReader reader = new StreamReader(importPath))
            {
                string line;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        string[] arrEmployee = line.Split(',');
                        string no = arrEmployee[0].Trim();
                        string name = arrEmployee[1].Trim();
                        string email = arrEmployee[2].Trim();
                        string passWord = arrEmployee[3].Trim();
                        bool isManager = Convert.ToBoolean(arrEmployee[4].Trim());
                        Employee empEnter = new Employee(no, name, email, passWord, isManager);
                        connector.InsertTable(connector.EmployeeTb, empEnter);
                    }
                }
                while (line != null);
            }
        }
        internal int CheckLogin()
        {
            Console.WriteLine(" Please Enter Your Email");
            string emailEnter = Console.ReadLine();
            Console.WriteLine(" Please Enter Your Password");
            string passwordEnter = Console.ReadLine();
            int check = connector.CheckLogin(emailEnter, passwordEnter);         
            return check;
        }
        public Employee EnterEmployee() 
        {            
            Employee empEnter = null;
            bool isStop = false;
            while (! isStop)
            {
                Console.WriteLine(" Fill information Employee according to format: no,name,email,passWord,isManager or exit");
                String empInfor = Console.ReadLine();
                if( empInfor == "exit" )
                {
                    isStop = true;
                }
                else
                {
                    int numberChar = 0;
                    foreach (char ch in empInfor)
                    {
                        if (ch == ',')
                        {
                            numberChar++;
                        }
                    }
                    if (numberChar == 4)
                    {
                        string[] empInforArray = empInfor.Split(',');
                        string no = empInforArray[0].Trim();
                        string name = empInforArray[1].Trim();
                        string email = empInforArray[2].Trim();
                        string passWord = empInforArray[3].Trim();
                        bool isManager = false;
                        try
                        {
                            isManager = Convert.ToBoolean(empInforArray[4].Trim());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(" Error: " + ex.Message);
                            continue;
                        }

                        isStop = true;
                        empEnter = new Employee(no, name, email, passWord, isManager);

                    }
                    else
                    {
                        Console.WriteLine(" Please enter enough field ");
                    }
                }
                
            }
            return empEnter;            
        }
    }
}
