using System;
using System.Globalization;

namespace ItemTemplateWizard.UI {
    public partial class PageFormWizardForm : BaseWizardForm {

        private string _caption;
        private string _datatype;
        private string _pagename;
        private int _priority;
        private bool _registerFolder;

        public PageFormWizardForm() {
            InitializeComponent();
            _priority = 100000;
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

        public string get_PageName()
        {
            return _pagename;
        }

        public bool get_RegisterFolder()
        {
            return _registerFolder;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _caption = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var text = textBox2.Text;
            if (String.IsNullOrEmpty(text))
                _pagename = String.Empty;
            _pagename = text + (!text.EndsWith("Page") ? "Page" : "");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            _datatype = textBox3.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            _priority = (int)numericUpDown1.Value;
        }

        protected override bool IsDataCorrect()
        {
            if (!IsDataCorrect(_caption, "Tytuł zakładki jest wymagany") ||
                !_registerFolder && !IsDataCorrect(_datatype, "Typ danych jest wymagany") ||
                !_registerFolder && !IsDataCorrect(_pagename, "Nazwa zakładki jest wymagana"))
                return false;
            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _registerFolder = checkBox1.Checked;
            textBox2.Enabled = textBox3.Enabled  = !_registerFolder;
        }

        private void PageFormWizardForm_Load(object sender, EventArgs e)
        {
            _registerFolder = false;
        }
    }
}
