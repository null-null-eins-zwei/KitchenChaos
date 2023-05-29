using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZZOT.KitchenChaos.Character;

namespace ZZOT.KitchenChaos
{
    public class TutorialUI : MonoBehaviour
    {
        [Header("Key Bindings")]
        [SerializeField] TextMeshProUGUI _bindMoveUpText;
        [SerializeField] TextMeshProUGUI _bindMoveLeftText;
        [SerializeField] TextMeshProUGUI _bindMoveDownText;
        [SerializeField] TextMeshProUGUI _bindMoveRightText;

        [Space(10)]
        [SerializeField] TextMeshProUGUI _bindInteractionText;
        [SerializeField] TextMeshProUGUI _bindGamepadInteractionText;

        [Space(10)]
        [SerializeField] TextMeshProUGUI _bindInteractionAltText;
        [SerializeField] TextMeshProUGUI _bindGamepadInteractionAltText;

        [Space(10)]
        [SerializeField] TextMeshProUGUI _bindPauseText;
        [SerializeField] TextMeshProUGUI _bindGamepadPauseText;

        private void Start()
        {
            UpdateVisual();
            UserInput.Instance.OnBindingRebind += UserInput_OnBindingRebind;
            KitchenGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

            Show();
        }

        private void GameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if(KitchenGameManager.Instance.IsCountdownToStartActive)
            {
                Hide();
            }
        }

        private void UserInput_OnBindingRebind(object sender, System.EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
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

        private void Show() => gameObject.SetActive(true);

        private void Hide() => gameObject.SetActive(false);
    }
}
