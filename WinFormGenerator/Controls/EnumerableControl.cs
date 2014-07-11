using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormGenerator.Controls
{
    class EnumerableControl : BaseControl
    {
        public EnumerableControl(object baseObject, Config config) : base(baseObject, config)
        {

        }

        public override Control GetControl(PropertyInfo propertyInfo)
        {
            Type listValueType = null;
            if (propertyInfo.PropertyType.IsGenericType &&
                propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof (List<>))
            {
                listValueType = propertyInfo.PropertyType.GetGenericArguments()[0];
            }
            else if (propertyInfo.PropertyType.IsArray)
            {
                listValueType = propertyInfo.PropertyType.GetElementType();
            }

            var panel = new Panel {Width = Config.DefaultControlWidth, Height = 130};
            var listbox = new ListBox {Height = 100, Width = Config.DefaultControlWidth};

            if (BaseObject != null)
            {
                var list = propertyInfo.GetValue(BaseObject);
                if (list != null)
                {
                    foreach (var s in (IEnumerable) list)
                    {
                        listbox.Items.Add(s);
                    }
                }
            }

            panel.Controls.Add(listbox);
            AddButtonAdd(panel,listValueType,propertyInfo,listbox);
            AddButtonEdit(panel, listValueType, propertyInfo, listbox);
            AddButtonRemove(panel, listValueType, propertyInfo, listbox);
            return panel; 
        }

        private void AddButtonAdd(Panel panel, Type listValueType, PropertyInfo propertyInfo, ListBox listbox)
        {
            var buttonAdd = new Button
            {
                Text = "Add",
                Location = new Point(listbox.Location.X, listbox.Location.Y + listbox.Height),
                Width = 60
            };
            buttonAdd.Click += (sender, args) => ButtonAddClick(listValueType, propertyInfo, listbox);
            panel.Controls.Add(buttonAdd);
        }

        private void AddButtonEdit(Panel panel, Type listValueType, PropertyInfo propertyInfo, ListBox listbox)
        {
            var buttonAdd = panel.Controls[panel.Controls.Count - 1]; 
            var buttonEdit = new Button
            {
                Text = "Edit",
                Location = new Point(buttonAdd.Location.X + buttonAdd.Width, listbox.Location.Y + listbox.Height),
                Width = 60
            };
            buttonEdit.Click += (sender, args) => ButtonEditClick(listValueType, propertyInfo, listbox);
            panel.Controls.Add(buttonEdit);
        }

        private void AddButtonRemove(Panel panel, Type listValueType, PropertyInfo propertyInfo, ListBox listbox)
        {
            var buttonEdit = panel.Controls[panel.Controls.Count - 1]; 
            var buttonRemove = new Button
            {
                Text = "Remove",
                Location = new Point(buttonEdit.Location.X + buttonEdit.Width, listbox.Location.Y + listbox.Height),
                Width = 60
            };

            buttonRemove.Click += (sender, args) => ButtonRemoveClick(listbox, propertyInfo.GetValue(BaseObject));
            panel.Controls.Add(buttonRemove);
        }

        private void ButtonAddClick(Type listValueType, PropertyInfo propertyInfo, ListBox listbox)
        {
            var form = Generator.GenerateWinForm(listValueType);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (propertyInfo.GetValue(BaseObject) == null)
                {
                    if (propertyInfo.PropertyType.IsGenericType)
                    {
                        propertyInfo.SetValue(BaseObject,
                            Activator.CreateInstance(propertyInfo.PropertyType));
                    }
                    else if (propertyInfo.PropertyType.IsArray)
                    {
                        propertyInfo.SetValue(BaseObject, Activator.CreateInstance(propertyInfo.PropertyType,
                            new object[] { 100 }));
                    }
                }

                var list = propertyInfo.GetValue(BaseObject);

                if (propertyInfo.PropertyType.IsGenericType)
                {
                    propertyInfo.PropertyType.GetMethod("Add").Invoke(list, new[] {form.Object});
                }
                else
                {
                    propertyInfo.PropertyType.GetMethod("SetValue").Invoke(list, new object[] {0});
                }
                
                listbox.Items.Add(form.Object.ToString());
            }
        }

        private void ButtonEditClick(Type listValueType, PropertyInfo propertyInfo, ListBox listbox)
        {
            if (listbox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select an item first.", "Select item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            
            var list = propertyInfo.GetValue(BaseObject);
            var value = propertyInfo.PropertyType.GetProperty("Item").GetValue(list, new object[] {listbox.SelectedIndex});

            var form = Generator.GenerateWinForm(listValueType,value);
            if (form.ShowDialog() == DialogResult.OK)
            {
                propertyInfo.PropertyType.GetProperty("Item").SetValue(list, form.Object, new object[] { listbox.SelectedIndex });
                listbox.Items[listbox.SelectedIndex] = form.Object;
            }
        }

        private void ButtonRemoveClick(ListBox listbox, object list)
        {
            if (listbox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select an item first.", "Select item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            list.GetType().GetMethod("RemoveAt").Invoke(list, new object[] { listbox.SelectedIndex });
            listbox.Items.RemoveAt(listbox.SelectedIndex);
        }


        public override Control GetControl(Type type)
        {
            throw new Exception("No value class control");
        }
    }
}
