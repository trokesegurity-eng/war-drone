namespace WarAquaDrone.Gameplay.Progression
{
    public sealed class EconomyService
    {
        private readonly PlayerProfile _profile;

        public EconomyService(PlayerProfile profile) => _profile = profile;

        public bool TrySpendCredits(int amount)
        {
            if (amount <= 0) return true;
            if (_profile.Credits < amount) return false;

            _profile.AddCredits(-amount);
            return true;
        }

        public void GrantCredits(int amount) => _profile.AddCredits(amount);
    }
}
