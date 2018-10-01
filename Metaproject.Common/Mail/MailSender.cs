using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Channels;

namespace Metaproject
{
    public class MailSender
    {
        public void Send(Data data)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(data.MailFrom),
                Subject = data.Subject,
                Body = data.Body,
                Priority = data.IsHighPriority ? MailPriority.High : MailPriority.Normal,
                IsBodyHtml = data.IsBodyHtml
            };

            if (data.SendTo.IsAnyItem())
            {
                foreach (string address in data.SendTo)
                    mailMessage.To.Add(address);
            }

            if (data.Bcc.IsAnyItem())
            {
                foreach (string address in data.Bcc)
                    mailMessage.Bcc.Add(address);
            }

            if (data.Cc.IsAnyItem())
            {
                foreach (string address in data.Cc)
                    mailMessage.CC.Add(address);
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = data.Server;
            smtpClient.Send(mailMessage);
        }

        public class Data : ICloneable
        {
            public string MailFrom { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Server { get; set; }
            public bool IsHighPriority { get; set; }
            public bool IsBodyHtml { get; set; }

            public List<string> SendTo { get; set; } = new List<string>();
            public List<string> Bcc { get; set; } = new List<string>();
            public List<string> Cc { get; set; } = new List<string>();

            public object Clone()
            {
                Data cloned = (Data)this.MemberwiseClone();
                cloned.SendTo = this.SendTo.ToList();
                cloned.Bcc = this.Bcc.ToList();
                cloned.Cc = this.Cc.ToList();
                return cloned;
            }
        }
    }
}