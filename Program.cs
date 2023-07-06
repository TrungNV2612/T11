using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T10_20230704
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            EmployeeManager manager = new EmployeeManager("Employee Manager");
            int selected;
            int checkLogin = manager.CheckLogin();
            if (checkLogin == 1)
            {
                do
                {
                    Console.WriteLine("***EMPLOYEE MANAGER***");
                    Console.WriteLine("1. Search Employee by Name or EmpNo or Email");
                    Console.WriteLine("2. Add New Employee");
                    Console.WriteLine("3. Update Employee");
                    Console.WriteLine("4. Delete Employee");
                    Console.WriteLine("5. ViewAll Employee");
                    Console.WriteLine("6. Export Employee");
                    Console.WriteLine("7. Import Employee");
                    Console.WriteLine("8. Exit");
                    Console.Write("   Select (1-8): ");
                    selected = Convert.ToInt16(Console.ReadLine());
                    switch (selected)
                    {
                        case 1:
                            Console.Clear();
                            manager.Find();
                            break;
                        case 2:
                            Console.Clear();                            
                            manager.AddNew();
                            break;
                        case 3:
                            Console.Clear();
                            manager.Update();
                            break;
                        case 4:
                            Console.Clear();
                            manager.Delete();
                            break;
                        case 5:
                            Console.Clear();
                            manager.ViewAll();
                            break;
                        case 6:
                            Console.Clear();
                            manager.Export();
                            break;
                        case 7:
                            Console.Clear();
                            manager.Import();
                            break;
                        case 8:
                            Console.Clear();
                            Console.WriteLine("-------- END ---------");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid");                            
                            break;
                    }
                }
                while (selected != 8);
            }
            else if (checkLogin == 2)
            {
                do
                {
                    Console.WriteLine("***EMPLOYEE MANAGER***");
                    Console.WriteLine("1. Search Employee by Name or EmpNo");
                    Console.WriteLine("2. ViewAll Employee");
                    Console.WriteLine("3. Exit");
                    Console.Write("   Select (1-3): ");
                    selected = Convert.ToInt16(Console.ReadLine());
                    switch (selected)
                    {
                        case 1:
                            Console.Clear();
                            manager.Find();
                            break;
                        case 2:
                            Console.Clear();
                            manager.ViewAll();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("-------- END ---------");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid");
                            break;
                    }
                }
                while (selected != 3);
            }
            else 
            {
                //Console.Clear(); 
                Console.WriteLine(" Check YourEmail or PassWord "); 
            }
        }
    }
}
