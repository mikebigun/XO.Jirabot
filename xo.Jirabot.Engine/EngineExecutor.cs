using xo.Jirabot.Contracts.Controllers;
using xo.Jirabot.Engine.Controllers;

namespace xo.Jirabot.Engine
{
    public class EngineExecutor
    {
        private IJiraController __jiraController;

        public EngineExecutor()
        {
            __jiraController = new JiraController();
        }

        public void RunJiraObserver()
        {
            __jiraController.SearchJira();
        }

        public void RunMattermostObserver()
        {
            // run mattermost api
        }

        public void RunSchedulesObserver()
        {
            // runs every min and checks Failed/Completed tasks
            // for every completed task it gets frequency of query
            // and creates new Planned task

            __jiraController.CreateJiraTasks();
        }
    }
}
