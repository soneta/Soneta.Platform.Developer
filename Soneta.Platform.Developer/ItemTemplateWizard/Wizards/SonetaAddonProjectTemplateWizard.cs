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
        private string destinationDir;
        private string projectSuffix;
        private bool configProject;
        private WizardRunKind wizardRunKind;
        private const string SolutionItemsFolderName = "Solution Items";

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            if (project == null)
                return;

            project.Name = Path.ChangeExtension(project.Name, projectSuffix);

            if (configProject)
            {
                var solutionItemsFolder = GetProjects(SolutionItemsFolderName).FirstOrDefault() ?? ((Solution2)dte.Solution).AddSolutionFolder(SolutionItemsFolderName);

                foreach (ProjectItem item in project.ProjectItems)
                {
                    var sourcePath = Path.Combine(destinationDir, item.Name);
                    var targetPath = Path.Combine(solutionDir, item.Name);
                    File.Move(sourcePath, targetPath);
                    solutionItemsFolder.ProjectItems.AddFromFile(targetPath);
                }

                dte.Solution.Remove(project);

                if (destinationDir == solutionDir)
                    File.Delete(project.FullName);
                else
                    Directory.Delete(destinationDir, true);
            }
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            if (wizardRunKind != WizardRunKind.AsMultiProject || destinationDir == solutionDir)
                return;

            var solution = dte.Solution;

            var projectsPaths = GetProjects().Except(GetProjects(SolutionItemsFolderName)).Select(project =>
            {
                solution.Remove(project);
                return project.FullName;
            }).ToArray();

            var targetDir = Path.GetDirectoryName(destinationDir);
            var tempDir = destinationDir + "Temp";
            Directory.Move(destinationDir, tempDir);

            foreach (var projectPath in projectsPaths)
            {
                var projectFileName = Path.GetFileName(projectPath);
                var projectName = Path.GetFileNameWithoutExtension(projectPath);
                var sourceProjectDir = Path.Combine(tempDir, projectName);
                var targetProjectDir = Path.Combine(targetDir, projectName);
                var targetProjectFileName = Path.Combine(targetProjectDir, projectFileName);

                Directory.Move(sourceProjectDir, targetProjectDir);
                solution.AddFromFile(targetProjectFileName);
            }

            Directory.Delete(tempDir, true);
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            dte = (DTE)automationObject;
            wizardRunKind = runKind;
            replacementsDictionary.TryGetValue("$solutiondirectory$", out solutionDir);
            replacementsDictionary.TryGetValue("$destinationdirectory$", out destinationDir);
            replacementsDictionary.TryGetValue("$projectsuffix$", out projectSuffix);
            configProject = projectSuffix == "Config";
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private IEnumerable<Project> GetProjects(string name = null)
        {
            var projects = dte.Solution.Projects.Cast<Project>();

            if (!string.IsNullOrEmpty(name))
                projects = projects.Where(x => x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));

            return projects;
        }
    }
}
