//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WinFormGenerator.Attributes;

namespace WinFormGenerator
{
    public class Generator
    {
        public static Config Config { get; set; }

        static Generator()
        {
            Config = new Config();
        }


        public static ClassForm GenerateWinForm(Type type, object obj = null)
        {
            var form = new ClassForm(type,obj);

            if (obj == null)
            {
                obj = Activator.CreateInstance(type);
            }

            LoadAttributs(type, form);

            var properties = type.GetProperties();
            var longestName = properties.Length > 0 ? properties.Max(p => p.Name.Length) : 0;

            GenerateControls(type,obj, properties, form, longestName);

            var formWidth = GetFormWidth(longestName, type,form); 

            if (formWidth < Config.MinimumFormWidth)
                formWidth = Config.MinimumFormWidth;

            form.Height = GetFormHeight(form); 
            form.Width = formWidth;
            form.Controls[1].Location = new Point(form.Width - form.Controls[0].Width*2 - Config.FormMargin ,form.Height - 75);
            form.Controls[0].Location = new Point(form.Width - form.Controls[0].Width - Config.FormMargin, form.Height - 75);

            return form;
        }

        private static int GetFormWidth(int longestName, Type type, ClassForm form)
        {
            if (type.IsValueType || type == typeof (string))
            {
                return form.Controls[2].Width + Config.FormMargin*2; 
            }
            else
            {
                return longestName * Config.PixelPerCharacter + Config.DefaultControlWidth + Config.FormMargin * 2;
            }
        }

        private static int GetFormHeight(ClassForm form)
        {
            return form.Controls[form.Controls.Count - 1].Location.Y +
                        Config.ControlMargin + Config.FormMargin + form.Controls[0].Height + 40;
        }

        private static void GenerateControls(Type type, object obj, PropertyInfo[] properties, ClassForm form, int biggestName)
        {
            if (type.IsValueType || type == typeof(string))
            {
                GenerateValueControls(type, obj, form);
            }
            else
            {
                GenerateClassControls(obj, properties,form,biggestName);
            }
        }


        private static void GenerateValueControls(Type type, object obj, ClassForm form)
        {
            var control = ControlFactory.GetControlFromValue(type, obj, Config); 
            control.Location = new Point(Config.FormMargin,Config.FormMargin);
            form.Controls.Add(control);
        }

        private static void GenerateClassControls(object obj, PropertyInfo[] properties, ClassForm form,
            int biggestName)
        {
            int latestY = 0;
            var listOfIndexes = new List<int>();

            for (int n = 0; n < properties.Length; n++)
            {
                var propertyInfo = properties[n];

                var attribute = propertyInfo.GetCustomAttribute(typeof(FormIgnoreAttribute)) as FormIgnoreAttribute;

                if(attribute != null)
                    continue;

               listOfIndexes.Add(n);

                var label = new Label
                {
                    Text = propertyInfo.Name,
                    Width = propertyInfo.Name.Length * Config.PixelPerCharacter,
                    Location = new Point(Config.FormMargin, Config.ControlMargin + latestY)
                };
                form.Controls.Add(label);
                var control = ControlFactory.GetControlFromProperty(obj, propertyInfo, Config);
                control.Location = new Point(biggestName * Config.PixelPerCharacter + Config.FormMargin, label.Location.Y);
                form.Controls.Add(control);
                latestY = control.Location.Y + control.Height;
            }

            form.ListOfIndex = listOfIndexes; 
        }

        private static void LoadAttributs(Type objType, ClassForm form)
        {
            var formAttribute = objType.GetCustomAttribute(typeof (FormAttribute), true) as FormAttribute;
            if (formAttribute != null)
            {
                form.Text = formAttribute.Text;
            }
        }
    }
}
