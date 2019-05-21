using System;
using System.Globalization;
using System.Windows.Forms;

namespace ItemTemplateWizard.UI {
    public partial class WorkerWizardForm : BaseWizardForm {

        private string _caption;
        private string _datatype;
        private int _priority;
        private bool _generateParams;

        public WorkerWizardForm() {
            InitializeComponent();
            _priority = 10000;
        }

        public string get_Caption()
        {
            return _caption;
        }

        public string get_DataType()
        {
            return _datatype;
        }

        public string get_Priority()
        {
            return _priority.ToString(CultureInfo.InvariantCulture);
        }

        public bool get_GenerateParams() {
            return _generateParams;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _caption = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            _datatype = textBox2.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            _priority = (int)numericUpDown1.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            _generateParams = checkBox1.Checked;
            textBox1.Enabled = _generateParams;
        }

        protected override bool IsDataCorrect()
        {
            if (!IsDataCorrect(_datatype, "Typ danych dla rejestrowanego workera jest wymagany") || 
                _generateParams && !IsDataCorrect(_caption, "Tytuł zakładki parametrów jest wymagany"))
                return false;
            return true;
        }

        private void WorkerWizardForm_Load(object sender, EventArgs e)
        {
            _generateParams = false;
        }

    }
}
