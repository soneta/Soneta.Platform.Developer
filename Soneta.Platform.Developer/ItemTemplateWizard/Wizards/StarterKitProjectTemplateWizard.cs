using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace ItemTemplateWizard.Wizards
{
    public class StarterKitProjectTemplateWizard : IWizard
    {
        private bool _addDbExtension;
        public void BeforeOpeningFile(ProjectItem projectItem) { } 
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { } 
        public void RunFinished() { } 

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams) 
        {

            var result = MessageBox.Show(null, "Czy uzupełnić projekt o elementy rozszerzenia bazy danych ?", "Uzupełnienie dodatku", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            _addDbExtension = result == DialogResult.Yes;
        }

        public void ProjectFinishedGenerating(Project project)
        {
            if (_addDbExtension)
                return;

            foreach (ProjectItem p in project.ProjectItems) {
                if (p.Name.Equals("Generator", StringComparison.InvariantCultureIgnoreCase)) {
                    p.Remove();
                    for (short i = 0; i < p.FileCount; i++) {
                        var f = p.FileNames[i];
                        Directory.Delete(f, true);
                    }
                }

                if (p.Name.Equals("Soneta.Generator.exe", StringComparison.InvariantCultureIgnoreCase)) {
                    p.Remove();
                    for (short i = 0; i < p.FileCount; i++) {
                        var f = p.FileNames[i];
                        File.Delete(f);
                    }
                }
            }
        }

        public bool ShouldAddProjectItem(string filePath) {
            return true;
        } 
    }
}
