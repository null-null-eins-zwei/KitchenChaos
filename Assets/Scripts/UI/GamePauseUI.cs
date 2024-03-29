using UnityEngine;
using UnityEngine.UI;

namespace ZZOT.KitchenChaos
{
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] Button _resumeButton;
        [SerializeField] Button _mainMenuButton;
        [SerializeField] Button _optionsButton;

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

            _optionsButton.onClick.AddListener(() =>
            {
                OpionsUI.Instance.Show(this.Show);
                Hide(); // to not confuse buttons navigation
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
            _resumeButton.Select(); // need to select because of gamepad/cursors
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
