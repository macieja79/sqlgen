using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlGen;
using TreasuryIS.Practices;
using TreasuryIS.WinForms.Tools;

namespace DockSample
{
    public partial class OptionsForm : ToolWindow, IParameterDialog
    {
        private readonly Guid _commandGuid;
        private readonly IEventAggregator _aggregator;

        public OptionsForm(Guid commandGuid, IEventAggregator aggregator)
        {
            _commandGuid = commandGuid;
            _aggregator = aggregator;
            InitializeComponent();
        }

        public void AttachParameters(object parameters)
        {
            var control = PropertyFormGenerator.Instance.CreateControlFor(parameters, new PropertyFormGenerator.Options(),
                null);
            control.Dock = DockStyle.Top;
            control.Height = 300;

            Controls.Add(control);

        }

        public event DialogClosedEventHandler DialogClosed;

        private void button1_Click(object sender, EventArgs e)
        {
            _aggregator.PublishEvent(new OptionsCommitedAggEvent{CommandId = _commandGuid });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (null != DialogClosed)
                DialogClosed(DialogStatus.Cancel);

        }
    }
}
