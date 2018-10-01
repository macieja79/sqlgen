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
	public partial class CodeGeneratorGridForm : ToolWindow, ISubscriber<ParametersRequestedAggEvent<CodeGeneratorGridParameters>>
	{

        private readonly IClipboardExcelDataProvider _clipboardExcelDataProvider;
	    private readonly IEventAggregator _eventAggregator;

	    #region <ctor>
        public CodeGeneratorGridForm(IClipboardExcelDataProvider clipboardExcelDataProvider, IEventAggregator eventAggregator)
		{

            _clipboardExcelDataProvider = clipboardExcelDataProvider;
		    _eventAggregator = eventAggregator;

            _eventAggregator.SubsribeEvent(this);
		    InitializeComponent();
            
        }
		#endregion

		#region <handlers>
	

		private void grid_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.V)
			{
				paste();
				return;
			}
		}

		



	

		void button5_Click(object sender, EventArgs e)
		{
			addColumn();
		}

		

	
		

		#endregion

		#region <prv>



		void paste()
		{

            string[,] data = _clipboardExcelDataProvider.GetFromClipboard();

            int rowCount = data.GetUpperBound(1) + 1;
			int colCount = data.GetUpperBound(0) + 1;

            grid.Rows.Clear();
            grid.Columns.Clear();

            for (int c = 0; c < colCount; c++)
			{
				grid.Columns.Add(c.ToString(), c.ToString());
			}

			for (int r = 0; r < rowCount; r++)
			{
				int rowIndex = grid.Rows.Add();
				grid.Rows[r].HeaderCell.Value = rowIndex;
				
				for (int c = 0; c < colCount; c++)
				{
					grid[c, r].Value = (string)data[c,r];
				}
			}
		}

		

				
		void addColumn()
		{

			int n = grid.Columns.Count;
				grid.Columns.Add(n.ToString(), n.ToString());
		}
        

		#endregion

		

		

	

		bool _isClipped = false;

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            paste();
        }

        public void OnEventHandler(ParametersRequestedAggEvent<CodeGeneratorGridParameters> e)
        {
            string[,] data = new string[grid.ColumnCount, grid.RowCount];

            for (int r = 0; r < grid.RowCount; r++)
            {
                for (int c = 0; c < grid.ColumnCount; c++)
                {
                    data[c, r] = (string)grid[c, r].Value;
                }
            }

            e.Parameters.Data = data;
        }
    }
}
