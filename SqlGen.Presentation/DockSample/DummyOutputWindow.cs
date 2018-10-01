using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DockSample
{
    public partial class DummyOutputWindow : ToolWindow
    {
        public DummyOutputWindow()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            textBox1.Text = "";

        }

        public void AddContent(string content)
        {
            textBox1.Text += content;
        }

        internal void AddContent(List<string> lines)
        {
            textBox1.Lines = lines.ToArray();
        }
    }
}