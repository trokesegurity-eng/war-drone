using System.Collections.Generic;

namespace WarAquaDrone.Gameplay.Progression
{
    public sealed class UpgradeService
    {
        private readonly HashSet<string> _unlockedNodes = new();

        public bool IsUnlocked(string nodeId) => _unlockedNodes.Contains(nodeId);
        public void Unlock(string nodeId) => _unlockedNodes.Add(nodeId);
    }
}
