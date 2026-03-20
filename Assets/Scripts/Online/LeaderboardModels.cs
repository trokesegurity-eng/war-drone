using System;

namespace SubDrone.Online
{
    [Serializable]
    public sealed class LeaderboardEntry
    {
        public string playerId;
        public string displayName;
        public int score;
        public string country;
        public string mode;
        public string biome;
        public string runId;
        public long timestampUnix;
    }
}
