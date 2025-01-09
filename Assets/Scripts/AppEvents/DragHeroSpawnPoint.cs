using frameworks.services.events;
using UnityEngine;

namespace towerdefence.events
{
    public class DragHeroSpawnPoint : AppEvent
    {
        public string HeroId;
        public bool IsDragging;

        public DragHeroSpawnPoint(string heroId, bool isDragging)
        {
            HeroId = heroId;
            IsDragging = isDragging;
        }
    }
}