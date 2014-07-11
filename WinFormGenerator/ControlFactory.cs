using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using WindowsFormsApplication2.Attributes;
using WindowsFormsApplication2.Controls;

namespace WindowsFormsApplication2
{
    public static class ControlFactory
    {
        public static Control GetControlFromProperty(object obj, PropertyInfo propertyInfo, Config config)
        {
            var baseControl = GetBaseControl(propertyInfo.PropertyType, obj, config);
            return baseControl.GetControl(propertyInfo);
        }

        public static Control GetControlFromValue(Type type, object obj, Config config)
        {
            var baseControl = GetBaseControl(type, obj, config);
            return baseControl.GetControl(type);
        }

        private static BaseControl GetBaseControl(Type type, object obj, Config config)
        {
            if (type == typeof(string))
            {
                return new StringBaseControl(obj, config);
            }
            if (type == typeof(int) || type == typeof(double) || type == typeof(float)
                || type == typeof(long) || type == typeof(short) || type == typeof(decimal))
            {
                return new NumberBaseControl(obj, config);
            }
            if (type.IsEnum)
            {
                return new EnumBaseControl(obj, config);
            }
            if (type == typeof(bool))
            {
                return new BoolBaseControl(obj, config);
            }
            if (type.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                return new EnumerableControl(obj, config);
            }
            if (type.IsClass)
            {
                return new ClassBaseControl(obj, config);
            }

            throw new Exception("No baseControl for that type.");
        }
    }
}
