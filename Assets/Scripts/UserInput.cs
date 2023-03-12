using UnityEngine;

namespace ZZOT.KitchenChaos.UserInputSystem
{
    public class UserInput : MonoBehaviour
    {
        UserInputActions _userInputActions;

        private void Awake()
        {
            _userInputActions = new UserInputActions();
            _userInputActions.Player.Enable();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 input = _userInputActions.Player.Move.ReadValue<Vector2>();

            // we need normalize because of diagonal cases,
            return input.normalized;
        }
    }
}
