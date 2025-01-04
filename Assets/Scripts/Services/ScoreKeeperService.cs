using frameworks.services;
using System.Threading.Tasks;

namespace towerdefence.services
{
    public class ScoreKeeperService : BaseService
    {
        public const int MAX_SCORE = 100;
        public const int DAMAGE_DEDUCTION = 10;

        public int LevelScore { get; private set; }

        public override Task Init()
        {
            ResetScore();
            return base.Init();
        }

        public void ResetScore()
        {
            LevelScore = MAX_SCORE;
        }

        public void SetupLevel()
        {
            ResetScore();
        }

        public void ReduceScore()
        {
            LevelScore -= DAMAGE_DEDUCTION;
        }

        public bool HasScoreLeft()
        {
            return LevelScore > 0;
        }
    }
}