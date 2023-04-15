using UnityEngine.SceneManagement;

namespace ZZOT.KitchenChaos
{
    public static class Loader
    {
        public enum Scene: int
        {
            MainMenuScene = 0,
            GameScene = 1,
            LoadingScreenScene = 2,
        }

        private static Scene targetScene;

        public static void Load(Scene scene)
        {
            targetScene = scene;
            SceneManager.LoadScene(Scene.LoadingScreenScene.ToString());
        }

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}
