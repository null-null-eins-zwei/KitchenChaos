using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace ZZOT.KitchenChaos.Character
{
    public class UserInput : MonoBehaviour
    {
        public static UserInput Instance {  get; private set; }

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;

        UserInputActions _userInputActions;

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
    }
}
