using System;
using System.Windows.Forms;
using SqlGen;

namespace DockSample
{
    public class ClipboardExcelDataProvider : IClipboardExcelDataProvider
    {
        public string[,] GetFromClipboard()
        {
            string text = Clipboard.GetText();
            text = text.Replace("\"", "");

            var data = GetClipData(text);
            return data;
        }

        private string[,] GetClipData(string text)
        {

            string[] lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            string[,] data = null;

            int colCount = -1;
            int rowCount = -1;


            for (int r = 0; r < lines.Length; r++)
            {
                string line = lines[r];

                string[] lineData = line.Split('\t');
                if (null == data)
                {
                    rowCount = lines.Length;
                    colCount = lineData.Length;
                    data = new string[colCount, rowCount];
                }
                else
                {
                    if (lineData.Length != colCount) continue;
                }

                for (int c = 0; c < colCount; c++)
                {

                    data[c, r] = lineData[c];
                }

            }

            return data;



        }
    }
}
