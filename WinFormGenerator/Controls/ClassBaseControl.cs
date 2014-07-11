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
    class ClassBaseControl : BaseControl
    {
        public ClassBaseControl(object baseObject, Config config) : base(baseObject, config)
        {
        }

        public override Control GetControl(PropertyInfo propertyInfo)
        {
            var button = new Button
            {
                Name = propertyInfo.Name,
                Text = string.Format("Edit {0}", propertyInfo.Name),
                Width = Config.DefaultControlWidth
            };

            button.Click += (sender, args) =>
            {
                var form = Generator.GenerateWinForm(propertyInfo.PropertyType,propertyInfo.GetValue(BaseObject));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    propertyInfo.SetValue(BaseObject,form.Object);
                }
            };

            return button; 
        }

        public override Control GetControl(Type type)
        {
            throw new Exception("No value class control");
        }
    }
}
