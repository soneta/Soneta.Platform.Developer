using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ItemTemplateWizard.UI {
    public partial class GeneratorVersionWizardForm : BaseWizardForm {

        public const string V1005Key = "1005";
        public const string V1006Key = "1006";
        public const string V1100Key = "1100";
        public const string V1101Key = "1101";

        public const string V1005 = "1005";
        public const string V1006 = "1006";
        public const string V1100 = "1100";
        public const string V1101 = "1101";

        public const string BtnText = "Nie dodawaj rozszerzenia";
        public static string ActualVersion = V1100;

        private readonly Dictionary<string, string>_supportedVersions = new Dictionary<string, string> {
            { V1005Key, V1005 },
            { V1006Key, V1006 },
            { V1100Key, V1100 },
            { V1101Key, V1101 }
        };

        public GeneratorVersionWizardForm() {
            InitializeComponent();
            button2.Text = BtnText;
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(_supportedVersions.Keys.Cast<object>().ToArray());
            checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(ActualVersion), true);
        }

        public string get_VersionExtension() {
            if (checkedListBox1.CheckedItems.Count == 0)
                return "";
            var d = checkedListBox1.CheckedItems[0] as string;
            return d == null ? "" : _supportedVersions[d];
        }

        protected override void button2_Click(object sender, EventArgs e) {
            DataCorrect = true;
            Close();
        }

        protected override bool IsDataCorrect() {
            return IsDataCorrect("Wersja rozszerzenia bazy danych jest wymagana",
                () => {
                    return checkedListBox1.CheckedItems.Count == 1;
                });
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (e.NewValue != CheckState.Checked || checkedListBox1.CheckedItems.Count <= 0)
                return;

            checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
            checkedListBox1.SetItemChecked(checkedListBox1.CheckedIndices[0], false);
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }

    }
}
