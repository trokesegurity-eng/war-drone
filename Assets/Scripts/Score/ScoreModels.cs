using System;

namespace SubDrone.Score
{
    [Serializable]
    public sealed class ScoreBreakdown
    {
        public int baseScore;
        public int timeBonus;
        public int integrityBonus;
        public int collisionPenalty;
        public int total;
    }
}
