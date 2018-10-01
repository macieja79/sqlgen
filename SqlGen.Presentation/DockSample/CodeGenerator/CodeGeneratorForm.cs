using SqlGen;
using SqlGen.Core.CSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Metaproject.Common.Practices;
using DockSample;
using SqlGen.Core.Presentation.Events;
using TreasuryIS.Practices;

namespace CodeGenerator
{
	public partial class CodeGeneratorForm : ToolWindow, ISubscriber<ParametersRequestedAggEvent<CodeGeneratorParameters>>
    {

        
        private readonly IEventAggregator _eventAggregator;

        #region <ctor>
        public CodeGeneratorForm(IEventAggregator eventAggregator)
		{
            _eventAggregator = eventAggregator;

            _eventAggregator.SubsribeEvent(this);

		    InitializeComponent();
            
        }
		#endregion

		#region <handlers>
		private void button1_Click(object sender, EventArgs e)
		{
			generateCode();
			

		}

		

		

		private void button3_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	

		

		

	
		

		#endregion

		#region <prv>

	

		void generateCode()
		{
			//string pattern = textBox1.Text;
            
   //         List<string> hedaderLines = textBox_header.Lines.ToList();

   //         var codeGenParams = new CodeGeneratorParameters
   //         {
   //             //Data = data,
   //             Pattern = pattern
   //         };

   //         var script = _codeGenerator.Generate(codeGenParams);
   //         hedaderLines.AddRange(script.Lines);

   //         List<string> footerLines = textBox_footer.Lines.ToList();
   //         hedaderLines.AddRange(footerLines);

   ////         textBox2.Lines = hedaderLines.ToArray();
			////tabControl1.SelectedTab = tabPage2;
		}

		
        

		#endregion
        
        public void OnEventHandler(ParametersRequestedAggEvent<CodeGeneratorParameters> e)
        {
            e.Parameters.Header = textBox_header.Text;
            e.Parameters.Pattern = textBox1.Text;
            e.Parameters.Footer = textBox_footer.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _eventAggregator.PublishEvent(new OptionsCommitedAggEvent());
        }
    }
}
