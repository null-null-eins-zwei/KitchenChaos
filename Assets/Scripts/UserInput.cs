using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace ZZOT.KitchenChaos.UserInputSystem
{
    public class UserInput : MonoBehaviour
    {
        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;

        UserInputActions _userInputActions;

        private void Awake()
        {
            _userInputActions = new UserInputActions();
            _userInputActions.Player.Enable();

            _userInputActions.Player.Interact.performed += Interact_performed;
            _userInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        }


        private void Interact_performed(CallbackContext context) =>
            OnInteractAction?.Invoke(this, EventArgs.Empty);

        private void InteractAlternate_performed(CallbackContext obj) =>
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 input = _userInputActions.Player.Move.ReadValue<Vector2>();

            // we need to normalize because of diagonal cases,
            return input.normalized;
        }
    }
}
