using WarAquaDrone.App;

namespace WarAquaDrone.UI.Screens
{
    public sealed class MenuScreen : UIScreen
    {
        public void OpenHangar() => GameManager.I.StateMachine.EnterHangar();
        public void StartMission(string sceneName) => GameManager.I.StateMachine.EnterMission(sceneName);
    }
}
