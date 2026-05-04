using System.Collections.Generic;

namespace WarAquaDrone.Gameplay.Progression
{
    public sealed class InventoryService
    {
        private readonly HashSet<string> _ownedParts = new();

        public bool OwnsPart(string partId) => _ownedParts.Contains(partId);
        public void UnlockPart(string partId) => _ownedParts.Add(partId);
    }
}
