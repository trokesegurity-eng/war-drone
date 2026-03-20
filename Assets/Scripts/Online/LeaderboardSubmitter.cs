using System;
using System.Threading;
using UnityEngine;
using SubDrone.Core;
using SubDrone.Score;

namespace SubDrone.Online
{
    public sealed class LeaderboardSubmitter : MonoBehaviour
    {
        public MonoBehaviour clientBehaviour;
        public string displayName = "Player";
        public string country = "BR";

        private ILeaderboardClient _client;
        private ScoreSystem _scoreSystem;

        private void Awake()
        {
            _client = clientBehaviour as ILeaderboardClient;
            _scoreSystem = FindObjectOfType<ScoreSystem>();
        }

        public async void SubmitLastScore()
        {
            if (_client == null || _scoreSystem == null || _scoreSystem.LastScore == null)
            {
                return;
            }

            var context = GameContext.Instance;
            var entry = new LeaderboardEntry
            {
                playerId = SystemInfo.deviceUniqueIdentifier,
                displayName = displayName,
                score = _scoreSystem.LastScore.total,
                country = country,
                mode = context != null ? context.Mode.ToString() : "Unknown",
                biome = context != null ? context.Biome.ToString() : "Unknown",
                runId = Guid.NewGuid().ToString("N"),
                timestampUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            };

            try
            {
                await _client.SubmitScoreAsync(entry, CancellationToken.None);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Submit failed: {exception}");
            }
        }
    }
}
