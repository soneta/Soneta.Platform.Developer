using System.Collections.Generic;
using System.Windows.Forms;
using EnvDTE;
using ItemTemplateWizard.UI;
using Microsoft.VisualStudio.TemplateWizard;

namespace ItemTemplateWizard.Wizards
{
    public class DashboardItemTemplateWizard : BaseItemTemplateWizard, IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem) { }
        public void ProjectFinishedGenerating(Project project) { }
        public bool ShouldAddProjectItem(string filePath) { return true; }
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { } 
        public void RunFinished() { }
        
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams) 
        {
            Dte = (DTE)automationObject;

            var form = new DashboardWizardForm();
            var result = form.ShowDialog();
            if (result != DialogResult.OK)
                throw new WizardCancelledException();

            // add an entry to the dictionary to specify the string used for the $viewinfotablename$ token 
            replacementsDictionary.Add("$dashboard_priority$", form.get_Priority());
            replacementsDictionary.Add("$dashboard_caption$", form.get_Caption());

            initDefaultNamespace(replacementsDictionary);
        }

    }
}
