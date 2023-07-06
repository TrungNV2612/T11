using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T10_20230704
{
    internal class Employee
    {
        internal string no;
        internal string name;
        internal string email;
        internal string password;
        internal bool isManager;
        internal Employee(string no, string name, string email, string password, bool isManager) 
        {
            this.no = no;
            this.name = name;
            this.email = email;
            this.password = password;
            this.isManager = isManager;
        }        
        public string getNo() 
        { 
            return this.no; 
        }
        public string getName()
        {
            return this.name;
        }
        public string getEmail()
        {
            return this.email;
        }
        public override string ToString()
        {
            return this.no + ", " + this.name + ", " + this.email;
        }
        public string ToStringAll()
        {
            //return this.no + ", " + this.name + ", " + this.email + ", " + this.password + ", " + this.isManager;
            return string.Format("{0}, {1}, {2}, {3}, {4}", this.no, this.name, this.email, this.password, this.isManager);
        }
    }
}
