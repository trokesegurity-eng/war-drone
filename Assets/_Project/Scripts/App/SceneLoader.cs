using UnityEngine.SceneManagement;

namespace WarAquaDrone.App
{
    public static class SceneLoader
    {
        public static void Load(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName)) return;
            SceneManager.LoadScene(sceneName);
        }
    }
}
