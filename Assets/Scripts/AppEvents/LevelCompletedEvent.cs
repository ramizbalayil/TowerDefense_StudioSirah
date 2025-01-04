using frameworks.services.events;

namespace towerdefence.events
{
    public class LevelCompletedEvent : AppEvent
    {
        public bool Won;

        public LevelCompletedEvent(bool won)
        { 
            Won = won;
        }
    }
}