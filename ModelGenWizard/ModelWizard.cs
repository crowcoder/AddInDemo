using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.ComponentModel;

namespace ModelGenWizard
{
    public class ModelWizard : IWizard
    {
        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem) { }

        public void ProjectFinishedGenerating(EnvDTE.Project project) { }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem) { }

        public void RunFinished() { }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            WizardUI wi = new WizardUI();
            wi.Closing += delegate (object sender, CancelEventArgs e)
            {
                var vm = wi.DataContext as ViewModel;
                replacementsDictionary.Add("$dynamicContent$", vm.GeneratedCode);
            };
            wi.ShowDialog();

        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
