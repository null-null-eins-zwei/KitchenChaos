using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        [SerializeField] Button _bindInteractionAltButton;
        [SerializeField] TextMeshProUGUI _bindInteractionAltText;

        [Header("Key Bindings Gamepad")]
        [SerializeField] Button _bindGamepadPauseButton;
        [SerializeField] TextMeshProUGUI _bindGamepadPauseText;

        [Space(5)]
        [SerializeField] Button _bindGamepadInteractionButton;
        [SerializeField] TextMeshProUGUI _bindGamepadInteractionText;

        [Space(5)]
        [SerializeField] Button _bindGamepadInteractionAltButton;
        [SerializeField] TextMeshProUGUI _bindGamepadInteractionAltText;

        [Space(10)]
        [SerializeField] Transform _rebindSplash;
        // Bindings ---

        [Header("Close Option Menu")]
        [SerializeField] Button _closeButton;

        private Action _onCloseButtonAction;

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

            _closeButton.onClick.AddListener(() =>
            {
                Hide();
                _onCloseButtonAction?.Invoke();
            });

            _bindMoveUpButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Move_Up));
            _bindMoveDownButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Move_Down));
            _bindMoveLeftButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Move_Left));
            _bindMoveRightButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Move_Right));

            _bindInteractionButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Interaction));
            _bindInteractionAltButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.InteractionAlt));

            _bindPauseButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Pause));

            _bindGamepadPauseButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Gamepad_Pause));
            _bindGamepadInteractionButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Gamepad_Interaction));
            _bindGamepadInteractionAltButton.onClick.AddListener(() => RebindBinding(UserInput.Binding.Gamepad_InteractionAlt));
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            UpdateVisual();
            Hide();
            HideRebindSplash();
        }

        private void OnDestroy()
        {
            KitchenGameManager.Instance.OnGameUnpaused -= GameManager_OnGameUnpaused;
        }

        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
        {
            Hide();
        }

        private void RebindBinding(UserInput.Binding binding)
        {
            ShowRebindSplash();
            UserInput.Instance.RebindBinding(
                binding,
                () => 
                {
                    HideRebindSplash();
                    UpdateVisual();
                });
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
            _bindGamepadInteractionText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Gamepad_Interaction);

            _bindInteractionAltText.text = UserInput.Instance.GetBindingText(UserInput.Binding.InteractionAlt);
            _bindGamepadInteractionAltText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Gamepad_InteractionAlt);

            _bindPauseText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Pause);
            _bindGamepadPauseText.text = UserInput.Instance.GetBindingText(UserInput.Binding.Gamepad_Pause);
        }

        private void ShowRebindSplash() => _rebindSplash.gameObject.SetActive(true);

        private void HideRebindSplash() => _rebindSplash.gameObject.SetActive(false);

        public void Show(Action onCloseButtonAction)
        {
            gameObject.SetActive(true);
            _closeButton.Select(); // need to select because of gamepad/cursors

            _onCloseButtonAction = onCloseButtonAction;
        }

        public void Hide() =>  gameObject.SetActive(false);
    }
}
