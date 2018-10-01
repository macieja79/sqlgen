using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metaproject
{
    public class MailHelper
    {
        public static void MailTo(string reciptient, string subject, string body)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            string fileName = string.Format("mailto:{0}?subject={1}&body={2}", reciptient, subject, body);
            proc.StartInfo.FileName = fileName;
            proc.Start();
        }

    }
}
