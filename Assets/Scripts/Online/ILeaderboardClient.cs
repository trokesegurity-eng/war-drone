using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SubDrone.Online
{
    public interface ILeaderboardClient
    {
        Task SubmitScoreAsync(LeaderboardEntry entry, CancellationToken cancellationToken);
        Task<IReadOnlyList<LeaderboardEntry>> GetTopAsync(string scope, int limit, CancellationToken cancellationToken);
    }
}
