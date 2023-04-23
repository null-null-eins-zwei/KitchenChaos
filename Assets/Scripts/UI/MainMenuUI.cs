using UnityEngine;
using UnityEngine.UI;

namespace ZZOT.KitchenChaos
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayClick);
            _quitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                Debug.LogWarning("Quit button pressed");
#else
                Application.Quit();
#endif
            });

            Time.timeScale = 1.0f;
        }

        private void PlayClick()
        {
            Loader.Load(Loader.Scene.GameScene);
        }
    }
}
