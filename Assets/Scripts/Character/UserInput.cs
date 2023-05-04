using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace ZZOT.KitchenChaos.Character
{
    public class UserInput : MonoBehaviour
    {
        public static UserInput Instance { get; private set; }

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;

        UserInputActions _userInputActions;

        public enum Binding
        {
            UNKNOWN = 0,
            Move_Up = 12,
            Move_Down = 14,
            Move_Left = 16,
            Move_Right = 18,

            Pause = 20,
            Interaction = 30,
            InteractionAlt = 32,
        }

        private void Awake()
        {
            Instance = this;

            _userInputActions = new UserInputActions();
            _userInputActions.Player.Enable();

            _userInputActions.Player.Interact.performed += Interact_performed;
            _userInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
            _userInputActions.Player.Pause.performed += Pause_performed;
        }

        private void OnDestroy()
        {
            _userInputActions.Player.Interact.performed -= Interact_performed;
            _userInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
            _userInputActions.Player.Pause.performed -= Pause_performed;

            _userInputActions.Dispose();
        }

        private void Pause_performed(CallbackContext context) =>
            OnPauseAction?.Invoke(this, EventArgs.Empty);

        private void Interact_performed(CallbackContext context) =>
            OnInteractAction?.Invoke(this, EventArgs.Empty);

        private void InteractAlternate_performed(CallbackContext context) =>
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 input = _userInputActions.Player.Move.ReadValue<Vector2>();

            // we need to normalize because of diagonal cases,
            return input.normalized;
        }

        public string GetBindingText(Binding binding) =>
                binding switch
                {
                    Binding.Move_Up => _userInputActions.Player.Move.bindings[1].ToDisplayString(),
                    Binding.Move_Down => _userInputActions.Player.Move.bindings[2].ToDisplayString(),
                    Binding.Move_Left => _userInputActions.Player.Move.bindings[3].ToDisplayString(),
                    Binding.Move_Right => _userInputActions.Player.Move.bindings[4].ToDisplayString(),

                    Binding.Interaction => _userInputActions.Player.Interact.bindings[0].ToDisplayString(),
                    Binding.InteractionAlt => _userInputActions.Player.InteractAlternate.bindings[0].ToDisplayString(),
                    
                    Binding.Pause => _userInputActions.Player.Pause.bindings[0].ToDisplayString(),
                    
                    _ => "UNKNOWN_BINGIND"!,
                };
    }
}
