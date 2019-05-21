using System.Collections.Generic;
using System.Windows.Forms;
using EnvDTE;
using ItemTemplateWizard.UI;
using Microsoft.VisualStudio.TemplateWizard;

namespace ItemTemplateWizard.Wizards
{
    public class PageFormItemTemplateWizard : BaseItemTemplateWizard, IWizard
    {
        private bool _registerFolder;
        public void BeforeOpeningFile(ProjectItem projectItem) { } 
        public void ProjectFinishedGenerating(Project project) { }
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { } 
        public void RunFinished() { } 

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams) 
        {
            Dte = (DTE)automationObject;

            var form = new PageFormWizardForm();
            var result = form.ShowDialog();
            if (result != DialogResult.OK)
                throw new WizardCancelledException();

            _registerFolder = form.get_RegisterFolder();
            // add an entry to the dictionary to specify the string used for the $viewinfotablename$ token 
            replacementsDictionary.Add("$pageform_caption$", form.get_Caption());
            replacementsDictionary.Add("$pageform_datattype$", form.get_DataType());
            replacementsDictionary.Add("$pageform_priority$", form.get_Priority());
            replacementsDictionary.Add("$pageform_pagename$", form.get_PageName());
            replacementsDictionary.Add("$pageform_registerfolder$", _registerFolder ? "1" : "0");
           
            initDefaultNamespace(replacementsDictionary);
       } 

        public bool ShouldAddProjectItem(string filePath) { return true; } 

    }
}
