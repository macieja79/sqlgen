using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Metaproject.Xml
{
    public class SerializerXml
    {

        #region <pub>

        public static string SerializeToXml<T>(T element)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            string str = string.Empty;

            using (TextWriter tw = new StringWriter())
            {
                serializer.Serialize(tw, element);
                str = tw.ToString();
            }

            return str;

        }

        public static T DeserializeFromXml<T>(string xmlStr)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            T t = default(T);

            using (TextReader tw = new StringReader(xmlStr))
            {
                try
                {

                    t = (T)serializer.Deserialize(tw);
                }
                catch 
                {
                    return t;
                }
            }

            return t;
        }
        #endregion

    }
}

