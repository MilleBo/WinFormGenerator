using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormGenerator.Controls
{
    public abstract class BaseControl
    {
        protected Config Config;
        protected object BaseObject; 

        protected BaseControl(object baseObject, Config config)
        {
            Config = config;
            BaseObject = baseObject;
        }

        public abstract Control GetControl(PropertyInfo propertyInfo);

        public abstract Control GetControl(Type type); 
    }
}
