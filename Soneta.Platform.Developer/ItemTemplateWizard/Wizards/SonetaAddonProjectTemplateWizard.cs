using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;

namespace ItemTemplateWizard.Wizards
{
    public class SonetaAddonProjectTemplateWizard : IWizard
    {
        private DTE dte;
        private string solutionDir;
        private string projectDir;
        private string projectSuffix;
        private bool configProject;
        const string solutionItemsFolderName = "Solution Items";

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            project.Name = Path.ChangeExtension(project.Name, projectSuffix);

            if (configProject)
            {
                var solutionItemsFolder = dte.Solution.Projects.Cast<Project>().FirstOrDefault(x => x.Name.Equals(solutionItemsFolderName, System.StringComparison.OrdinalIgnoreCase));
                solutionItemsFolder = solutionItemsFolder ?? ((Solution2)dte.Solution).AddSolutionFolder(solutionItemsFolderName);

                foreach (ProjectItem item in project.ProjectItems)
                {
                    var sourcePath = Path.Combine(projectDir, item.Name);
                    var targetPath = Path.Combine(solutionDir, item.Name);
                    File.Move(sourcePath, targetPath);
                    solutionItemsFolder.ProjectItems.AddFromFile(targetPath);
                }

                dte.Solution.Remove(project);

                if (projectDir == solutionDir)
                    File.Delete(project.FullName);
                else
                    Directory.Delete(projectDir, true);
            }
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            dte = (DTE)automationObject;
            replacementsDictionary.TryGetValue("$solutiondirectory$", out solutionDir);
            replacementsDictionary.TryGetValue("$destinationdirectory$", out projectDir);
            replacementsDictionary.TryGetValue("$projectsuffix$", out projectSuffix);
            configProject = projectSuffix == "Config";
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        } 
    }
}
