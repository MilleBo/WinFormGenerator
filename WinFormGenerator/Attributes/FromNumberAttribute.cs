using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    class FromNumberAttribute : Attribute
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }
}
