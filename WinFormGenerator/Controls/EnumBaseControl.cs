using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormGenerator.Controls
{
    class EnumBaseControl : BaseControl
    {

        public EnumBaseControl(object baseObject, Config config) : base(baseObject, config)
        {
            
        }

        public override Control GetControl(PropertyInfo propertyInfo)
        {
            var combobox = new ComboBox 
            { 
                Name = propertyInfo.Name, 
                Width = Config.DefaultControlWidth
            };
            combobox.Items.AddRange(Enum.GetNames(propertyInfo.PropertyType));
            combobox.SelectedIndex = 0;
            combobox.DropDownStyle = ComboBoxStyle.DropDownList;
            return combobox;
        }

        public override Control GetControl(Type type)
        {
            var combobox = new ComboBox
            {
                Name = "enum",
                Width = Config.DefaultControlWidth
            };
            combobox.Items.AddRange(Enum.GetNames(BaseObject.GetType()));
            combobox.SelectedIndex = 0;
            combobox.DropDownStyle = ComboBoxStyle.DropDownList;
            return combobox;
        }
    }
}
