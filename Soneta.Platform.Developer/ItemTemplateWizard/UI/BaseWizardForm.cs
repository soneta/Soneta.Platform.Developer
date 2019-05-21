using System;
using System.Windows.Forms;

namespace ItemTemplateWizard.UI {
    public partial class BaseWizardForm : Form {

        protected bool DataCorrect;

        protected BaseWizardForm() {
            InitializeComponent();
        }

        private void ViewInfoWizardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DataCorrect && MessageBox.Show(this, @"Czy przerwać dodawanie komponentu ?", Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }

        protected virtual bool IsDataCorrect(string value, string text) {
            if (!String.IsNullOrEmpty(value))
                return true;
            MessageBox.Show(this, text, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        protected virtual bool IsDataCorrect(string text, Func<bool> predicate) {
            if (predicate != null && predicate())
                return true;
            MessageBox.Show(this, text, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        protected virtual bool IsDataCorrect()
        {
            return true;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!IsDataCorrect())
                return;

            DialogResult = DialogResult.OK;
            DataCorrect = true;
            Close();

        }

        protected virtual void button2_Click(object sender, EventArgs e) {

        }

    }
}
