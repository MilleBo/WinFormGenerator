//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;
using System.Reflection;
using System.Windows.Forms;
using WinFormGenerator.Attributes;

namespace WinFormGenerator.Controls
{
    class StringBaseControl : BaseControl
    {
        public StringBaseControl(object baseObject, Config config) : base(baseObject, config)
        {
            
        }

        public override Control GetControl(PropertyInfo propertyInfo)
        {
            bool multiline = false;
            int rows = 1;

            var attribute = propertyInfo.GetCustomAttribute(typeof (FormTextAttribute)) as FormTextAttribute;
            if (attribute != null)
            {
                multiline = attribute.Multiline;
                rows = attribute.Rows;
            }

            string value = string.Empty;
            if (BaseObject != null)
            {
                var tempValue = propertyInfo.GetValue(BaseObject);
                if(tempValue != null)
                    value = tempValue.ToString(); 
            }
            return new TextBox
            {
                Name = propertyInfo.Name, 
                Width = Config.DefaultControlWidth, 
                Multiline = multiline, 
                Height = rows * 15,
                Text = value
            };
        }

        public override Control GetControl(Type type)
        {
            return new TextBox
            {
                Name = "string",
                Width = Config.DefaultControlWidth,
                Text = BaseObject != null ? BaseObject.ToString() : string.Empty
            };
        }
    }
}
