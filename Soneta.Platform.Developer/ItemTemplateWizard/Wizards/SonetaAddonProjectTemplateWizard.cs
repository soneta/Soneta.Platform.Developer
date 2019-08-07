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
        private const string SolutionItemsFolderName = "Solution Items";

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            if (projectSuffix == "Config")
                ProcessConfigFiles(project);
            else
                EnsureSuffix(project);
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
            replacementsDictionary.TryGetValue("$destinationdirectory$", out destinationDir);
            replacementsDictionary.TryGetValue("$projectsuffix$", out projectSuffix);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private void ProcessConfigFiles(Project project)
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

        private void EnsureSuffix(Project project)
        {
            if (string.IsNullOrEmpty(projectSuffix))
                return;

            project.Name = GetProjectNameWithSuffix(project);
            EnsureProjectDirectoryNameWithSuffix(project);
        }

        private string GetProjectNameWithSuffix(Project project)
        {
            var sourceExtension = Path.GetExtension(project.Name);
            var targetExtension = $".{projectSuffix}";

            if (sourceExtension.Equals(targetExtension, System.StringComparison.InvariantCultureIgnoreCase))
                return Path.ChangeExtension(project.Name, targetExtension);
            else
                return project.Name + targetExtension;
        }

        private void EnsureProjectDirectoryNameWithSuffix(Project project)
        {
            if (destinationDir == solutionDir || project.Name == Path.GetFileName(destinationDir))
                return;

            dte.Solution.Remove(project);
            var targetDir = Path.Combine(solutionDir, project.Name);
            Directory.Move(destinationDir, targetDir);
            dte.Solution.AddFromFile(Path.Combine(targetDir, Path.GetFileName(project.FullName)));
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
