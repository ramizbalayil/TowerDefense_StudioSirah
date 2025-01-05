using frameworks.services.events;

namespace towerdefence.events
{
    public class LevelSelectedEvent : AppEvent
    {
        public int Level;

        public LevelSelectedEvent(int level)
        {
            Level = level;
        }
    }
}