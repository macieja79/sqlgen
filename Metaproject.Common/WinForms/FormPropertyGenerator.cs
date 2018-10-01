using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using TreasuryIS.Practices;

namespace TreasuryIS.WinForms.Tools
{

        public class PropertyFormGenerator
        {


            public class Options
            {
                public Options()
                {
                    Edited = new List<string>();

                }
                public List<string> Edited { get; set; }

                
                public int? FirstColumnWidth { get; set; }

                public Dictionary<string, string> CheckedListBindigs { get; set; } = new Dictionary<string, string>();

            }

            #region <singleton>

            PropertyFormGenerator()
            {
            }

            static PropertyFormGenerator _instance;



            public static PropertyFormGenerator Instance
            {
                get
                {
                    if (null == _instance)
                        _instance = new PropertyFormGenerator();

                    return _instance;
                }
            }

            #endregion

            #region narzędzia

            ColorConverter _converter = new ColorConverter();

            /// <summary>
            /// Generowanie kontrolki zawierającą kontrolki do edycji wlasciwosci obiektu
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="options"></param>
            /// <param name="checkifToEdit"></param>
            /// <returns></returns>
            public TableLayoutPanel CreateControlFor(object obj, Options options, IEventAggregator aggregator)
            {
                TableLayoutPanel panel = new TableLayoutPanel();
                panel.ColumnCount = 2;
                panel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500));
                

                PropertyInfo[] properties = obj.GetType().GetProperties();

                int row = 0;
                int height = 0;
                foreach (PropertyInfo propInfo in properties)
                {
                    bool isToEdit = CheckIfToEdit(propInfo, options);
                    if (!isToEdit) continue;


                    string propertyName = propInfo.Name;
                    PropertyInfoAttribute propInfoAttribute =
                        propInfo.GetCustomAttribute(typeof(PropertyInfoAttribute)) as PropertyInfoAttribute;
                    if (null != propInfoAttribute)
                    {
                        propertyName = propInfoAttribute.DisplayName;
                    }

                    Label label = new Label();
                    label.Text = propertyName;
                    label.Dock = DockStyle.Fill;
                    panel.Controls.Add(label, 0, row);

                    Control control = null;
                    string name = propInfo.PropertyType.Name;

                    bool isArrayOfString = propInfo.PropertyType.IsArray &&
                                  propInfo.PropertyType.GetElementType() == typeof(string);

                if (propInfo.PropertyType.IsEnum)
                    {
                        ComboBox combo = new ComboBox();
                        combo.DataSource = Enum.GetValues(propInfo.PropertyType);
                        Binding binding = new Binding("SelectedItem", obj, propInfo.Name);
                        binding.Parse += BindingOnParse;
                        combo.DataBindings.Add("SelectedItem", obj, propInfo.Name);

                        control = combo;
                    }
                    else if (name == "Boolean")
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.Checked = (bool)propInfo.GetValue(obj);
                        checkBox.DataBindings.Add("Checked", obj, propInfo.Name);
                        control = checkBox;

                    }
                    else if (name == "Guid")
                    {
                        TextBox textBox = new TextBox();
                        textBox.Text = propInfo.GetValue(obj).ToString();
                        textBox.Dock = DockStyle.Fill;
                        textBox.Name = propInfo.Name;
                        control = textBox;
                    }
                    else if (name == "DateTime")
                    {
                        DateTimePicker picker = new DateTimePicker();
                        picker.DataBindings.Add("Value", obj, propInfo.Name);
                        control = picker;
                    }
                else if (isArrayOfString)
                {
                    TextBox textBox = new TextBox();

                    object objVal = propInfo.GetValue(obj);
                    string[] lines = (string[]) objVal;
                    textBox.Lines = lines;
                    textBox.Dock = DockStyle.Fill;
                    textBox.DataBindings.Add(nameof(TextBox.Lines), obj, propInfo.Name);
                    textBox.Multiline = true;
                    textBox.ScrollBars = ScrollBars.Both;
                    control = textBox;
                }
                else
                    {
                        TextBox textBox = new TextBox();
                        object objVal = propInfo.GetValue(obj);
                        string str = (null != objVal) ? objVal.ToString() : string.Empty;
                        textBox.Text = str;
                        textBox.Dock = DockStyle.Fill;
                        textBox.DataBindings.Add("Text", obj, propInfo.Name);
                        control = textBox;

                        if (propInfo.Name.Contains("Color"))
                        {
                            //textBox.ReadOnly = true;
                            Color color = (Color) _converter.ConvertFromString(str);
                            textBox.BackColor = color;
                            textBox.Text = propInfo.Name;
                            textBox.Click += TbOnClick;
                        }
                    }

                    height += control.Height;
                    panel.Controls.Add(control, 1, row);

                    row++;
                }

                panel.ColumnStyles[0].SizeType = SizeType.AutoSize;
                panel.ColumnStyles[1].SizeType = SizeType.Percent;
                panel.ColumnStyles[1].Width = 100;
            
                panel.RowCount = row;
                return panel;
            }
        
            private void BindingOnParse(object sender, ConvertEventArgs e)
            {


            }

            private bool CheckIfToEdit(PropertyInfo propInfo, Options options)
            {

                if (null == options) return true;
                if (options.Edited.IsNullOrEmpty()) return true;

                string name = propInfo.Name;
                if (options.Edited.Contains(name)) return true;

                return false;
            }

            private void TbOnClick(object sender, EventArgs eventArgs)
            {

                TextBox tb = (TextBox)sender;
                Color color = tb.BackColor;

                ColorDialog dialog = new ColorDialog();
                dialog.Color = color;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Color edited = dialog.Color;
                    string colorString = string.Format("#{0:X2}{1:X2}{2:X2}", edited.R, edited.G, edited.B);
                    tb.Text = colorString;
                    tb.BackColor = edited;
                    BindingOnParse(tb, null);
                }

            }

            /// <summary>
            /// Generowanie formatki zawierającej kontrolki do edycji wlasciwosci obiektu
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public Form CreateFormFor(object obj, string formName, IEventAggregator aggregator)
            {
                Form form = new Form();
                form.Text = formName;

                TableLayoutPanel panel = CreateControlFor(obj, null, aggregator);
                panel.Dock = DockStyle.Fill;
                form.Controls.Add(panel);


                Button okButton = new Button();
                okButton.DialogResult = DialogResult.OK;
                okButton.Text = "Ok";

                Button cancelButton = new Button();
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Text = "Anuluj";

                int nextRow = panel.RowCount + 1;
                panel.Controls.Add(okButton, 0, nextRow);
                panel.Controls.Add(cancelButton, 1, nextRow);

                form.Height = panel.Height + 20;

                form.FormClosing += form_FormClosing;

                form.Tag = obj;

                return form;
            }

            void form_FormClosing(object sender, FormClosingEventArgs e)
            {
                Form form = (Form)sender;

                object obj = form.Tag;
                PropertyInfo[] properties = obj.GetType().GetProperties();

                foreach (PropertyInfo propInfo in properties)
                {
                    string name = propInfo.PropertyType.Name;
                    if (name == "Guid")
                    {

                        Control c = null;
                        Control container = form.Controls[0];
                        foreach (Control control in container.Controls)
                        {
                            if (control.Name == propInfo.Name)
                            {
                                c = control;
                            }
                        }

                        if (null != c)
                        {
                            TextBox tb = (TextBox)c;
                            string guidStr = tb.Text;
                            Guid guid = new Guid(guidStr);
                            propInfo.SetValue(obj, guid);
                        }
                    }
                }
            }

            #endregion

        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class PropertyInfoAttribute : Attribute
        {

            public PropertyInfoAttribute(string displayName)
            {
                DisplayName = displayName;
            }

            public string DisplayName { get; set; }

        }

            

        public class CheckedListBoxItem
        {
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }

        

}





