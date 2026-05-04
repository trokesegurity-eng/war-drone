namespace WarAquaDrone.App
{
    public sealed class BootState : IGameState
    {
        private readonly AppConfig _cfg;
        public BootState(AppConfig cfg) => _cfg = cfg;
        public void Enter() => SceneLoader.Load(_cfg.menuScene);
        public void Exit() { }
    }

    public sealed class MenuState : IGameState
    {
        public MenuState(AppConfig cfg) { }
        public void Enter() { }
        public void Exit() { }
    }

    public sealed class HangarState : IGameState
    {
        private readonly AppConfig _cfg;
        public HangarState(AppConfig cfg) => _cfg = cfg;
        public void Enter() => SceneLoader.Load(_cfg.hangarScene);
        public void Exit() { }
    }

    public sealed class MissionState : IGameState
    {
        private readonly string _scene;
        public MissionState(string scene) => _scene = scene;
        public void Enter() => SceneLoader.Load(_scene);
        public void Exit() { }
    }
}
