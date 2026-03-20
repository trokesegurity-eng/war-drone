using UnityEngine;
using SubDrone.Data;
using SubDrone.Drone;

namespace SubDrone.Score
{
    public static class ScoreCalculator
    {
        public static ScoreBreakdown Calculate(MissionDefinition mission, DroneState drone)
        {
            var breakdown = new ScoreBreakdown
            {
                baseScore = mission != null ? mission.baseScore : 0,
            };

            if (mission == null || drone == null)
            {
                breakdown.total = breakdown.baseScore;
                return breakdown;
            }

            breakdown.timeBonus = Mathf.Max(0, Mathf.RoundToInt((600f - drone.elapsedSeconds) * mission.timeBonusPerSecond));
            var integrityPercent = Mathf.Clamp01(drone.integrity / 100f);
            breakdown.integrityBonus = Mathf.RoundToInt(integrityPercent * 100f * mission.integrityBonusPerPercent);
            breakdown.collisionPenalty = drone.collisions * mission.penaltyPerCollision;
            breakdown.total = Mathf.Max(0, breakdown.baseScore + breakdown.timeBonus + breakdown.integrityBonus - breakdown.collisionPenalty);
            return breakdown;
        }
    }
}
