using System.Collections.Generic;
using EnvDTE;
using System.Linq;

namespace ItemTemplateWizard.Wizards
{
    public class BaseItemTemplateWizard
    {
        protected DTE Dte;
        protected static readonly string[] ProjectProperties = { "defaultnamespace" };

        protected virtual void initDefaultNamespace(Dictionary<string, string> replacementsDictionary)
        {
            var project = (Project)((object[])Dte.ActiveSolutionProjects)[0];
            foreach (Property prop in project.Properties) {
                if (ProjectProperties.Contains(prop.Name.ToLower())) {
                    replacementsDictionary.Add("$" + prop.Name.ToLower() + "$",
                        (prop.Value == null ? string.Empty : prop.Value.ToString()));
                }
            }
            
        }
    }
}
