using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    class FormAttribute : Attribute
    {
        public string Text { get; set; }
    }
}
