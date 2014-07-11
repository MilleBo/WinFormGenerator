using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormGenerator
{
    public partial class ClassForm : Form
    {
        public object Object { get; set; }

        public Type ObjectType { get; set; }
        public List<int> ListOfIndex { get; set; }

        public ClassForm(Type objectType, object @object = null)
        {
            InitializeComponent();
            Object = @object;
            ObjectType = objectType;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Object == null && !ObjectType.IsValueType && ObjectType != typeof(string))
            {
                Object = Activator.CreateInstance(ObjectType);
            }

            if (ObjectType.IsValueType || ObjectType == typeof(string))
            {
                Object = Controls[2].Text;
            }

            var properties = ObjectType.GetProperties();
            int index = 0;
            for (int n = 0; n + 3 < Controls.Count; n += 2)
            {
                if (Controls[n + 3] is ComboBox)
                {
                    var combobox = Controls[n + 3] as ComboBox;
                    properties[ListOfIndex[index]].SetValue(Object,
                        Enum.Parse(properties[ListOfIndex[index]].PropertyType, combobox.SelectedItem.ToString()));
                }
                else if (Controls[n + 3] is CheckBox)
                {
                    var checkbox = Controls[n + 3] as CheckBox;
                    properties[ListOfIndex[index]].SetValue(Object, checkbox.Checked);
                }
                else if (Controls[n + 3] is Button)
                {
                    //Do nothing
                }
                else
                {
                    if (!string.IsNullOrEmpty(Controls[n + 3].Text))
                        properties[ListOfIndex[index]].SetValue(Object, Convert.ChangeType(Controls[n + 3].Text, properties[ListOfIndex[index]].PropertyType));

                }
                index++;
            }
        }
    }
}
