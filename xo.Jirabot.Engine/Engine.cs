using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities;

namespace xo.Jirabot.Engine
{
    public class Engine
    {
        public void GetSheduledTask()
        {
            var context = EngineContext.Instance();
            var logger = context.Logger;

            logger.WriteDebug("This is first debug message.");       
        }
    }
}
