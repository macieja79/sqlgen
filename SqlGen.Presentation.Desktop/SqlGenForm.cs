using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlGen.Presentation.Desktop
{
    public partial class SqlGenForm : Form
    {
        
        private readonly ITool<CsClassGeneratorParameters> _classGenerator;
        private readonly ITool<EfEdmxExtractorParameters> _efEdmxExtractor;
        private readonly ITool<SqlSeedScriptParams> _sqlSeedGenerator;

        public SqlGenForm(
            ITool<SqlSeedScriptParams> sqlSeedGenerator,
            ITool<CsClassGeneratorParameters> classGenerator,
            ITool<EfEdmxExtractorParameters> efEdmxExtractor
            )
        {
            _sqlSeedGenerator = sqlSeedGenerator;
            _classGenerator = classGenerator;
            _efEdmxExtractor = efEdmxExtractor;

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProceedGenerateScript();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProceedGenerateClass();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProceedGetEdmx();
        }
        
        void ProceedGenerateScript()
        {
            var sqlSeedParameters = new SqlSeedScriptParams
            {
                ConnectionString = textBox1.Text,
                SqlCommand = textBox2.Text.Split(Environment.NewLine.ToSingleElementArray(), StringSplitOptions.RemoveEmptyEntries),
                ChunkSize = (int) numericUpDown1.Value,
                IsToGenerateCreateTable = checkBox1.Checked,
                IsToGenerateInsertTable = checkBox2.Checked,
                IsToRecreateDatabase = checkBox3.Checked,
                TargetDatabaseName = textBox4.Text,
                MultiStatementSeparator = Environment.NewLine
            };

            using (var progress = new Metaproject.Controls.Progress())
            {
                var script = _sqlSeedGenerator.Generate(sqlSeedParameters);
                textBox3.Lines = script.Lines.ToArray();
            }
        }

        private void ProceedGenerateClass()
        {
            var lines = textBox2.Lines;
            var className = textBox4.Text;
            var script = _classGenerator.Generate(new CsClassGeneratorParameters{TargetClassName = className, PropertyNames = lines});
            textBox3.Lines = script.Lines.ToArray();
        }

        private void ProceedGetEdmx()
        {
            var migrationName = textBox4.Text;
            var connectionString = textBox1.Text;
            // TODO: put this into view
            var schemaName = "ritanl";

            var result = _efEdmxExtractor.Generate(new EfEdmxExtractorParameters{ConnectionString = connectionString, Schema = schemaName, MigrationName = migrationName});

            textBox3.Text = result.Lines.FirstOrDefault();

        }


    }
}
