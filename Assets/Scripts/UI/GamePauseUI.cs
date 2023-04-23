using UnityEngine;
using UnityEngine.UI;

namespace ZZOT.KitchenChaos
{
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] Button _resumeButton;
        [SerializeField] Button _mainMenuButton;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(() =>
            {
                KitchenGameManager.Instance.ResumeGame();
            });

            _mainMenuButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
            KitchenGameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            Hide();
        }

        private void OnDestroy()
        {
            KitchenGameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
            KitchenGameManager.Instance.OnGameUnpaused -= GameManager_OnGameUnpaused;
        }

        private void GameManager_OnGamePaused(object sender, System.EventArgs e) => Show();

        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) => Hide();

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
