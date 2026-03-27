namespace WarAquaDrone.App
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }

    public sealed class GameStateMachine
    {
        private readonly AppConfig _cfg;
        private IGameState _current;

        public GameStateMachine(AppConfig cfg) => _cfg = cfg;

        public void EnterBoot() => Switch(new BootState(_cfg));
        public void EnterMenu() => Switch(new MenuState(_cfg));
        public void EnterHangar() => Switch(new HangarState(_cfg));
        public void EnterMission(string missionScene) => Switch(new MissionState(missionScene));

        private void Switch(IGameState next)
        {
            _current?.Exit();
            _current = next;
            _current.Enter();
        }
    }
}
