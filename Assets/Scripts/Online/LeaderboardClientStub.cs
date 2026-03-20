using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SubDrone.Online
{
    /// <summary>Stub local para substituir por um backend real futuramente.</summary>
    public sealed class LeaderboardClientStub : MonoBehaviour, ILeaderboardClient
    {
        private readonly List<LeaderboardEntry> _entries = new();

        public Task SubmitScoreAsync(LeaderboardEntry entry, CancellationToken cancellationToken)
        {
            _entries.Add(entry);
            _entries.Sort((left, right) => right.score.CompareTo(left.score));
            Debug.Log($"[LB] Submit: {entry.displayName}={entry.score} scope={entry.country}");
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<LeaderboardEntry>> GetTopAsync(string scope, int limit, CancellationToken cancellationToken)
        {
            var list = new List<LeaderboardEntry>();
            for (var i = 0; i < _entries.Count && list.Count < limit; i++)
            {
                var entry = _entries[i];
                if (scope == "WORLD" || entry.country == scope)
                {
                    list.Add(entry);
                }
            }

            return Task.FromResult((IReadOnlyList<LeaderboardEntry>)list);
        }
    }
}
