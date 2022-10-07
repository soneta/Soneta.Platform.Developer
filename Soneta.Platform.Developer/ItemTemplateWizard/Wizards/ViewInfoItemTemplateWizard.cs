using System.Collections.Generic;
using System.Windows.Forms;
using EnvDTE;
using ItemTemplateWizard.UI;
using Microsoft.VisualStudio.TemplateWizard;

namespace ItemTemplateWizard.Wizards
{
    public class ViewInfoItemTemplateWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem) { } 
        public void ProjectFinishedGenerating(Project project) { } 
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { } 
        public void RunFinished() { }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams) 
        {

            var form = new ViewInfoWizardForm();
            var result = form.ShowDialog();
            if (result != DialogResult.OK)
                throw new WizardCancelledException();

            // add an entry to the dictionary to specify the string used for the $viewinfotablename$ token 
            replacementsDictionary.Add("$viewinfo_tablename$", form.get_TableName());
            replacementsDictionary.Add("$viewinfo_description$", form.get_Description());
            replacementsDictionary.Add("$viewinfo_priority$", form.get_Priority());
 
        } 

        public bool ShouldAddProjectItem(string filePath) { return true; } 
    }
}
