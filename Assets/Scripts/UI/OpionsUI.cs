using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZZOT.KitchenChaos.Character;

namespace ZZOT.KitchenChaos
{
    public class OpionsUI : MonoBehaviour
    {
        public static OpionsUI Instance { get; private set; }

        [Header("Volume")]
        [SerializeField] Button _soundEffectsButton;
        [SerializeField] TextMeshProUGUI _soundEffectsText;

        [SerializeField] Button _musicButton;
        [SerializeField] TextMeshProUGUI _musicButtonText;

        // Bindings +++
        [Header("Key Bindings")]
        [SerializeField] Button _bindMoveUpButton;
        [SerializeField] TextMeshProUGUI _bindMoveUpText;

        [Space(5)]
        [SerializeField] Button _bindMoveDownButton;
        [SerializeField] TextMeshProUGUI _bindMoveDownText;

        [Space(5)]
        [SerializeField] Button _bindMoveLeftButton;
        [SerializeField] TextMeshProUGUI _bindMoveLeftText;

        [Space(5)]
        [SerializeField] Button _bindMoveRightButton;
        [SerializeField] TextMeshProUGUI _bindMoveRightText;

        [Space(10)]
        [SerializeField] Button _bindPauseButton;
        [SerializeField] TextMeshProUGUI _bindPauseText;

        [Space(10)]
        [SerializeField] Button _bindInteractionButton;
        [SerializeField] TextMeshProUGUI _bindInteractionText;

        [Space(5)]
        [SerializeField] Button _bindInteractionAltutton;
        [SerializeField] TextMeshProUGUI _binInteractionAltText;
        // Bindings ---

        [Header("Close Option Menu")]
        [SerializeField] Button _closeButton;

        private void Awake()
        {
            Instance = this;

            _soundEffectsButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });

            _musicButton.onClick.AddListener(() =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });

            _closeButton.onClick.AddListener(Hide);
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            UpdateVisual();
            Hide();
        }

        private void OnDestroy()
        {
            KitchenGameManager.Instance.OnGameUnpaused -= GameManager_OnGameUnpaused;
        }

        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
        {
            Hide();
        }

        private void UpdateVisual()
        {
            _soundEffectsText.text = $"Sound Effects: {Mathf.Round(SoundManager.Instance.GetVolume() * 100f)}";
            _musicButtonText.text = $"Music: {Mathf.Round(MusicManager.Instance.GetVolume() * 100f)}";

            _bindMoveUpText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Move_Up);
            _bindMoveDownText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Move_Down);
            _bindMoveLeftText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Move_Left);
            _bindMoveRightText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Move_Right);

            _bindInteractionText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Interaction);
            _binInteractionAltText.text = UserInput.Instance.GetBindingText(UserInput.Binding.InteractionAlt);

            _bindPauseText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Pause);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide() 
        {
            gameObject.SetActive(false);
        }
    }
}
