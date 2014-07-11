using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class FormTextAttribute : Attribute
    {
        public bool Multiline { get; set; }
        public int Rows { get; set; }
    }
}
