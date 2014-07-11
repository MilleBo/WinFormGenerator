using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace WinFormGenerator.Controls
{
    class NumberBaseControl : BaseControl
    {
        public NumberBaseControl(object baseObject, Config config)
            : base(baseObject, config)
        {

        }

        public override Control GetControl(PropertyInfo propertyInfo)
        {
            var numeric = new NumericUpDown
            {
                Name = propertyInfo.Name,
                Width = Config.DefaultControlWidth,
            };

            if (propertyInfo.PropertyType == typeof(int))
            {
                if (BaseObject != null)
                    numeric.Value = Convert.ToInt32(propertyInfo.GetValue(BaseObject).ToString());
            }
            else if (propertyInfo.PropertyType == typeof(double))
            {
                numeric.DecimalPlaces = 1;
                if (BaseObject != null)
                    numeric.Value = (decimal)Convert.ToDouble(propertyInfo.GetValue(BaseObject).ToString());
            }
            else if (propertyInfo.PropertyType == typeof(float))
            {
                numeric.DecimalPlaces = 1;
                if (BaseObject != null)
                    numeric.Value = (decimal)float.Parse(propertyInfo.GetValue(BaseObject).ToString());
            }
            else if (propertyInfo.PropertyType == typeof(long))
            {
                if (BaseObject != null)
                    numeric.Value = long.Parse(propertyInfo.GetValue(BaseObject).ToString());
            }
            else if (propertyInfo.PropertyType == typeof(decimal))
            {
                if (BaseObject != null)
                    numeric.Value = Convert.ToDecimal(propertyInfo.GetValue(BaseObject).ToString());
            }
            else if (propertyInfo.PropertyType == typeof (short))
            {
                if (BaseObject != null)
                    numeric.Value = short.Parse(propertyInfo.GetValue(BaseObject).ToString());               
            }

            return numeric;
        }

        public override Control GetControl(Type type)
        {
            var numeric = new NumericUpDown
            {
                Name = "Number",
                Width = Config.DefaultControlWidth,
            };

            if (type == typeof(double))
            {
                numeric.DecimalPlaces = 1;
                if (BaseObject != null)
                    numeric.Value = (decimal)Convert.ToDouble(BaseObject);
            }
            else if (type == typeof(float))
            {
                numeric.DecimalPlaces = 1;
                if (BaseObject != null)
                    numeric.Value = (decimal)float.Parse(BaseObject.ToString());
            }
            else if (type == typeof(long))
            {
                if (BaseObject != null)
                    numeric.Value = long.Parse(BaseObject.ToString());
            }
            else if (type == typeof(decimal))
            {
                if (BaseObject != null)
                    numeric.Value = Convert.ToDecimal(BaseObject);
            }
            else if (type == typeof(short))
            {
                if (BaseObject != null)
                    numeric.Value = short.Parse(BaseObject.ToString());
            }

            return numeric;
        }
    }
}
