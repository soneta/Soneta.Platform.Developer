using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using EnvDTE;
using ItemTemplateWizard.UI;
using Microsoft.VisualStudio.TemplateWizard;
using System.Linq;

namespace ItemTemplateWizard.Wizards
{
    public class WorkerItemTemplateWizard : IWizard
    {
        private bool _registerWorkerParamatersClass;
        public void BeforeOpeningFile(ProjectItem projectItem) { }
        public bool ShouldAddProjectItem(string filePath) { return true; }
        public void RunFinished() { }
        
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams) 
        {
            _registerWorkerParamatersClass = false;

            var form = new WorkerWizardForm();
            var result = form.ShowDialog();
            if (result != DialogResult.OK)
                throw new WizardCancelledException();

            _registerWorkerParamatersClass = form.get_GenerateParams();

            // add an entry to the dictionary to specify the string used for the $viewinfotablename$ token 
            replacementsDictionary.Add("$worker_datattype$", form.get_DataType());
            replacementsDictionary.Add("$worker_priority$", form.get_Priority());
            replacementsDictionary.Add("$worker_params_caption$", form.get_Caption());
            replacementsDictionary.Add("$worker_params_register$", _registerWorkerParamatersClass ? "1" : "0");

        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            MessageBox.Show(projectItem.Name);
            if (_registerWorkerParamatersClass)
                return;

            if (projectItem.Name.EndsWith("pageform.xml")) {
                projectItem.Remove();
                for (short i = 0; i < projectItem.FileCount; i++) {
                    var f = projectItem.FileNames[i];
                    File.Delete(f);
                }
            }
        } 
    }
}
