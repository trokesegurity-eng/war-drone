using System.Collections.Generic;
using SubDrone.Data;

namespace SubDrone.Missions
{
    public sealed class MissionRuntime
    {
        public MissionRuntime(MissionDefinition definition)
        {
            Definition = definition;
            if (definition?.objectives == null)
            {
                return;
            }

            foreach (var objective in definition.objectives)
            {
                if (objective == null || string.IsNullOrWhiteSpace(objective.id))
                {
                    continue;
                }

                ProgressByObjectiveId[objective.id] = 0;
            }
        }

        public MissionDefinition Definition { get; }
        public Dictionary<string, int> ProgressByObjectiveId { get; } = new();

        public bool TryAdvance(string objectiveId, int amount = 1)
        {
            if (!ProgressByObjectiveId.ContainsKey(objectiveId))
            {
                return false;
            }

            ProgressByObjectiveId[objectiveId] += amount;
            return true;
        }

        public bool IsCompleted()
        {
            if (Definition?.objectives == null)
            {
                return false;
            }

            foreach (var objective in Definition.objectives)
            {
                if (objective == null)
                {
                    continue;
                }

                if (!ProgressByObjectiveId.TryGetValue(objective.id, out var progress) || progress < objective.targetCount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
