using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlGen.Presentation.Desktop.Progress
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        #region <nested types>
        public class OwnerWindowData
        {
            public OwnerWindowData(int width, int height, int x, int y)
            {
                Height = height;
                Width = width;
                X = x;
                Y = y;
            }

            public int Height { get; set; }
            public int Width { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

        }




        #endregion

 

        public void SetIcon(Icon PrgIcon)
        {
            //Bitmap Bmp = PrgIcon.ToBitmap();
            //PictureBox.Image = System.Drawing.Image.FromHbitmap(Bmp.GetHbitmap());
            //PictureBox.Update(); 
        }


        private delegate void InvokeSetVisible(bool Value);
        public void SetVisible(bool Value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeSetVisible(SetVisible), Value);
                return;
            }

            this.Visible = Value;
        }

        private delegate bool InvokeGetVisible();
        public bool GetVisible()
        {
            if (this.InvokeRequired)
                return (bool)this.Invoke(new InvokeGetVisible(GetVisible));

            return this.Visible;
        }

        private delegate void InvokeShowProgress(IWin32Window Parent);
        public void ShowProgress(IWin32Window Parent)
        {
            if (Parent == null)
                Parent = Form.ActiveForm;
            ShowProgress(Parent, Modal);
        }

        public void ShowProgress(IWin32Window Parent, bool Modal)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeShowProgress(ShowProgress), Parent);
                return;
            }



            bool IsParent = (Parent != null && Parent.Handle != null);
            IsParent = false;


            if (IsParent)
            {
                Control ctrl = Form.FromHandle(Parent.Handle);
                Point pt = ctrl.Location;

                this.StartPosition = FormStartPosition.Manual;
                this.Left = pt.X + ((ctrl.Width / 2) - this.Size.Width) / 2;
                this.Top = pt.Y + ((ctrl.Height / 2) - this.Size.Height) / 2;
            }
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Left = ((Screen.PrimaryScreen.Bounds.Width / 2) - this.Size.Width) / 2;
                this.Top = ((Screen.PrimaryScreen.Bounds.Height / 2) - this.Size.Height) / 2;
            }

            if (Steps == 0)
                progressBar1.Visible = false;
            else
                progressBar1.Visible = true;
            this.TopMost = true;

            if (IsParent)
            {
                this.Show(Parent);
                this.Invalidate(true);
                this.Refresh();
            }
            else if (!Modal)
            {
                this.Visible = true;
                this.Invalidate(true);
                //Logo.Update();
                //Logo.Refresh();
                //Logo.Invalidate();
            }
            else
                this.ShowDialog(Parent);
        }
        private delegate bool InvokeCloseProgress();
        public bool CloseProgress()
        {
            if (this.InvokeRequired)
                return (bool)this.Invoke(new InvokeCloseProgress(CloseProgress));

            LabelHeader = "";
            LabelText = "";
            Position = 0;



            if (this.Modal)
                this.Close();
            else
            {
                this.Visible = false;
            }

            return true;
        }



        #region <header>
        public string LabelHeader
        {
            get
            {
                return GetLabelHeader();
            }
            set
            {
                SetLabelHeader(value);
            }
        }

        private delegate void InvokeSetLabelHeader(string Value);
        private void SetLabelHeader(string Value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeSetLabelHeader(SetLabelHeader), Value);
                return;
            }


            LabelTop1.Text = Value;
            LabelTop1.Refresh();
            Refresh();

        }

        private delegate string InvokeGetLabelHeader();
        private string GetLabelHeader()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new InvokeGetLabelHeader(GetLabelHeader));

            Application.DoEvents();

            return LabelTop1.Text;

        }
        #endregion

        #region <label text>
        public string LabelText
        {
            get
            {
                return GetLabelText();
            }
            set
            {
                SetLabelText(value);
            }
        }

        private delegate void InvokeSetLabelText(string Value);
        private void SetLabelText(string Value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeSetLabelText(SetLabelText), Value);
                return;
            }

            Application.DoEvents();

            textBox1.Text = Value;
            textBox1.Refresh();
            Refresh();

        }

        private delegate string InvokeGetLabelText();
        private string GetLabelText()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new InvokeGetLabelText(GetLabelText));

            return textBox1.Text;

        }
        #endregion

        #region <steps>
        public int Steps
        {
            get
            {
                return GetSteps();
            }
            set
            {
                SetSteps(value);
            }
        }

        private delegate void InvokeSetSteps(int Value);
        private void SetSteps(int Value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeSetSteps(SetSteps), Value);
                return;
            }


            if (Value != 0)
                progressBar1.Visible = true;
            else
                progressBar1.Visible = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Value;
            progressBar1.Refresh();

        }



        private delegate int InvokeGetSteps();
        private int GetSteps()
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new InvokeGetSteps(GetSteps));

            return progressBar1.Maximum;

        }
        #endregion

        #region <position>
        public int Position
        {
            get
            {
                return GetPosition();
            }
            set
            {
                SetPosition(value);
            }
        }
        private delegate void InvokeSetPosition(int Pos);
        private void SetPosition(int Pos)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeSetPosition(SetPosition), Pos);
                return;
            }

            Application.DoEvents();


            progressBar1.Value = Pos;
            progressBar1.Refresh();

        }
        private delegate int InvokeGetPosition();
        private int GetPosition()
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new InvokeGetPosition(GetPosition));


            return progressBar1.Value;

        }
        #endregion

        private delegate void InvokeStep(int step);
        public void Step(int step)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeStep(Step), step);
                return;
            }

            Application.DoEvents();

            if (step < progressBar1.Maximum)
            {
                progressBar1.Value = step;
                progressBar1.Refresh();
            }

        }
        private delegate void InvokeStepBack();


     
      


    }
}
