using UnityEngine;
using SubDrone.Core;
using SubDrone.Drone;
using SubDrone.Score;
using SubDrone.Sonar;

namespace SubDrone.Missions
{
    /// <summary>Valida missões via scan e publica score quando concluídas.</summary>
    public sealed class MissionSystem : MonoBehaviour
    {
        public SonarSystem sonar;
        public DroneController2D drone;

        public MissionRuntime Runtime { get; private set; }

        private void Start()
        {
            var context = GameContext.Instance;
            if (context == null || context.Mission == null)
            {
                Debug.LogWarning("Sem missão no GameContext. MissionSystem ficará ocioso.");
                return;
            }

            Runtime = new MissionRuntime(context.Mission);
            if (sonar != null)
            {
                sonar.OnScanCompleted += HandleScanCompleted;
            }
        }

        private void OnDestroy()
        {
            if (sonar != null)
            {
                sonar.OnScanCompleted -= HandleScanCompleted;
            }
        }

        private void HandleScanCompleted(ISonarTarget target)
        {
            if (Runtime?.Definition == null)
            {
                return;
            }

            foreach (var objective in Runtime.Definition.objectives)
            {
                if (objective == null)
                {
                    continue;
                }

                if (objective.type == "ScanSpecies" && target.TargetKey == objective.targetKey)
                {
                    Runtime.TryAdvance(objective.id, 1);
                }
            }

            if (!Runtime.IsCompleted())
            {
                return;
            }

            var score = ScoreCalculator.Calculate(Runtime.Definition, drone != null ? drone.State : null);
            Debug.Log($"MISSION COMPLETE! Score={score.total}");

            var scoreSystem = FindObjectOfType<ScoreSystem>();
            if (scoreSystem != null)
            {
                scoreSystem.Publish(score);
            }
        }
    }
}
