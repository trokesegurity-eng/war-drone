using UnityEngine;

namespace SubDrone.Score
{
    /// <summary>Publica score final e expõe integração com leaderboard.</summary>
    public sealed class ScoreSystem : MonoBehaviour
    {
        public ScoreBreakdown LastScore { get; private set; }

        public void Publish(ScoreBreakdown score)
        {
            LastScore = score;
            Debug.Log($"Score Published: {score.total} (base={score.baseScore}, time={score.timeBonus}, integ={score.integrityBonus}, pen={score.collisionPenalty})");
        }
    }
}
