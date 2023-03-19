using System;
using UnityEngine.SceneManagement;

namespace Architecture.Services
{
    public class SceneLoader
    {
        public const string GameScene = "Game";
        public const string MenuScene = "Menu";

        public void LoadScene(string scene, Action callback = null)
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var op = SceneManager.LoadSceneAsync(scene);

            op.completed += operation =>
            {
                callback?.Invoke();
            };
        }
    }
}
