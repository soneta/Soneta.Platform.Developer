using System;
using System.Globalization;

namespace ItemTemplateWizard.UI {
    public partial class DashboardWizardForm : BaseWizardForm {

        private string _caption;
        private int _priority;

        public DashboardWizardForm() {
            InitializeComponent();
            _priority = 10000;
        }

        public string get_Caption()
        {
            return _caption;
        }

        public string get_Priority()
        {
            return _priority.ToString(CultureInfo.InvariantCulture);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _caption = textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            _priority = (int)numericUpDown1.Value;
        }

        protected override bool IsDataCorrect()
        {
            return IsDataCorrect(_caption, "Tytuł dla dashboard jest wymagany");
        }
    }
}
