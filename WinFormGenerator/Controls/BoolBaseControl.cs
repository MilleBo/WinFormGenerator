using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormGenerator.Controls
{
    class BoolBaseControl : BaseControl
    {
        public BoolBaseControl(object baseObject, Config config)
            :base(baseObject, config)
        {
        }

        public override Control GetControl(PropertyInfo propertyInfo)
        {
            return new CheckBox { Name = propertyInfo.Name, Checked = (bool) propertyInfo.GetValue(BaseObject)};
        }

        public override Control GetControl(Type type)
        {
            return new CheckBox {Name = "bool", Checked = (bool) BaseObject}; 
        }
    }
}
