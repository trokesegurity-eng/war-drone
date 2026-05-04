using UnityEngine;
using WarAquaDrone.App;
using WarAquaDrone.App.Save;
using WarAquaDrone.Core;
using WarAquaDrone.Gameplay.Missions.Objectives;

namespace WarAquaDrone.Gameplay.Missions
{
    public sealed class MissionManager : MonoBehaviour
    {
        [SerializeField] private MissionDefinition mission;
        [SerializeField] private ObjectiveBase[] objectives;

        private bool _completed;

        private void Start()
        {
            foreach (var objective in objectives) objective.Activate();
        }

        private void Update()
        {
            if (_completed || objectives == null || objectives.Length == 0) return;

            foreach (var objective in objectives)
            {
                if (!objective.IsComplete) return;
            }

            CompleteMission();
        }

        private void CompleteMission()
        {
            _completed = true;

            var profile = GameManager.I.Profile;
            profile.AddXP(mission.rewardXP);
            profile.AddCredits(mission.rewardCredits);

            EventBus.Publish(new MissionCompleted(mission.missionId, mission.rewardXP, mission.rewardCredits));
            Debug.Log($"Mission completed: {mission.missionId}");
        }
    }
}
