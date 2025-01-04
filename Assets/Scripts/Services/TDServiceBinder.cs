using frameworks.services;
using frameworks.services.events;
using frameworks.services.scenemanagement;

namespace towerdefence.services
{
    public class TDServiceBinder : ServiceBinder
    {
        protected async override void Start()
        {
            base.Start();
            // Initialize services here
            await ServiceRegistry.Bind<EventHandlerService>(new EventHandlerService());
            await ServiceRegistry.Bind<SceneManagementService>(new SceneManagementService());

            IsInitialized = true;
        }
    }
}