using System;
using WarAquaDrone.Core;

namespace WarAquaDrone.App.Save
{
    [Serializable]
    public class PlayerSave
    {
        public int level = 1;
        public int xp;
        public int credits;
        public string equippedDroneId = "drone_basic";
        public string equippedBiomeProfileId = "manta";
    }

    public readonly struct CreditsChanged : IGameEvent
    {
        public readonly int NewValue;
        public CreditsChanged(int newValue) => NewValue = newValue;
    }

    public readonly struct MissionCompleted : IGameEvent
    {
        public readonly string MissionId;
        public readonly int XP;
        public readonly int Credits;

        public MissionCompleted(string missionId, int xp, int credits)
        {
            MissionId = missionId;
            XP = xp;
            Credits = credits;
        }
    }
}
