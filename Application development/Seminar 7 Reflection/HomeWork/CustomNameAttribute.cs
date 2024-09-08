using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomNameAttribute : Attribute
    {
        public string CustomName { get; set; }

        public CustomNameAttribute(string customName)
        {
            CustomName = customName;
        }
    }
}
