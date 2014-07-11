//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;
using System.Reflection;
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
