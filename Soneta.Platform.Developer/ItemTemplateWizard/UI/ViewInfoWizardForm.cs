using System;
using System.Globalization;

namespace ItemTemplateWizard.UI {
    public partial class ViewInfoWizardForm : BaseWizardForm {

        private string _tableName;
        private string _description;
        private int _priority;

        public ViewInfoWizardForm() {
            InitializeComponent();
            _priority = 10000;
        }

        public string get_TableName()
        {
            return _tableName;
        }

        public string get_Description()
        {
            return _description;
        }

        public string get_Priority()
        {
            return _priority.ToString(CultureInfo.InvariantCulture);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _tableName = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            _description = textBox2.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _priority = (int)numericUpDown1.Value;
        }

        protected override bool IsDataCorrect() {
            return IsDataCorrect(_tableName, "Nazwa tabeli jest wymagana");
        }

    }
}
