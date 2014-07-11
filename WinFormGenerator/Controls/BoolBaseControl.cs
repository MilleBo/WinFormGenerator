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
