using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlGen
{
    public class WinFormsHelper
    {
        public static void Center(Form child, Form parent)
        {
            child.Left = (parent.ClientSize.Width - child.Width) / 2;
            child.Top = (parent.ClientSize.Height - child.Height) / 2;
        }

    }
}
