using UnityEngine;
using WarAquaDrone.App.Save;
using WarAquaDrone.Core;
using WarAquaDrone.Gameplay.Progression;

namespace WarAquaDrone.App
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager I { get; private set; }

        [SerializeField] private AppConfig config;

        public GameStateMachine StateMachine { get; private set; }
        public SaveService Save { get; private set; }
        public PlayerProfile Profile { get; private set; }
        public EconomyService Economy { get; private set; }
        public InventoryService Inventory { get; private set; }
        public UpgradeService Upgrades { get; private set; }

        private void Awake()
        {
            if (I != null)
            {
                Destroy(gameObject);
                return;
            }

            I = this;
            DontDestroyOnLoad(gameObject);

            Save = new SaveService();
            Profile = new PlayerProfile(Save.LoadOrCreate());
            Economy = new EconomyService(Profile);
            Inventory = new InventoryService();
            Upgrades = new UpgradeService();

            ServiceLocator.Register(Profile);
            ServiceLocator.Register(Economy);
            ServiceLocator.Register(Inventory);
            ServiceLocator.Register(Upgrades);

            StateMachine = new GameStateMachine(config);
            StateMachine.EnterBoot();
        }

        private void OnApplicationPause(bool paused)
        {
            if (paused) Save.Save(Profile.ToSave());
        }

        private void OnApplicationQuit()
        {
            Save.Save(Profile.ToSave());
        }
    }
}
