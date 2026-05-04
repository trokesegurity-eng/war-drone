using WarAquaDrone.App.Save;
using WarAquaDrone.Core;

namespace WarAquaDrone.Gameplay.Progression
{
    public sealed class PlayerProfile
    {
        public int Level { get; private set; }
        public int XP { get; private set; }
        public int Credits { get; private set; }

        public string EquippedDroneId { get; private set; }
        public string EquippedBiomeProfileId { get; private set; }

        public PlayerProfile(PlayerSave save)
        {
            Level = save.level;
            XP = save.xp;
            Credits = save.credits;
            EquippedDroneId = save.equippedDroneId;
            EquippedBiomeProfileId = save.equippedBiomeProfileId;
        }

        public void AddCredits(int amount)
        {
            Credits += amount;
            EventBus.Publish(new CreditsChanged(Credits));
        }

        public void AddXP(int amount)
        {
            XP += amount;
            while (XP >= Level * 100)
            {
                XP -= Level * 100;
                Level++;
            }
        }

        public void EquipDrone(string id) => EquippedDroneId = id;
        public void EquipBiomeProfile(string id) => EquippedBiomeProfileId = id;

        public PlayerSave ToSave()
        {
            return new PlayerSave
            {
                level = Level,
                xp = XP,
                credits = Credits,
                equippedDroneId = EquippedDroneId,
                equippedBiomeProfileId = EquippedBiomeProfileId
            };
        }
    }
}
