using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T10_20230704
{
    internal class BaseManager
    {
        
        internal BaseManager(string name) 
        {
            Console.WriteLine(name);
        }
        internal virtual void Find()
        {
            Console.WriteLine(" Find ");
        }
        internal virtual void AddNew()
        {
            Console.WriteLine(" AddNew ");
        }
        internal virtual void Update()
        {
            Console.WriteLine(" Update ");
        }
        internal virtual void Delete()
        {
            Console.WriteLine(" Delete ");
        }
        internal virtual void ViewAll()
        {
            Console.WriteLine(" ViewAll ");
        }
        internal virtual void Export()
        {
            Console.WriteLine(" Export ");
        }
        internal virtual void Import()
        {
            Console.WriteLine(" Import ");
        }
    }
}
