using UnityEngine;
using ZZOT.KitchenChaos.UserInputSystem;

namespace ZZOT.KitchenChaos.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 7f;
        [SerializeField] private UserInput _input;

        private readonly float _rotateSpeed = 10f;
        private bool _isWalking = false;

        // Update is called once per frame
        private void Update()
        {
            // deltaTime required because of variable framerate
            Vector2 input = _playerSpeed * Time.deltaTime * _input.GetMovementVectorNormalized();

            Vector3 move = new(
                            x: input.x,
                            y: 0f,
                            z: input.y);


            _isWalking = move != Vector3.zero;

            transform.position += move;

            Vector3 lookAt = Vector3.Slerp(
                                 transform.forward,
                                 move,
                                 Time.deltaTime * _rotateSpeed // this is strange, there is better ways to determine rotation speed
                                 );

            //rotation = Quaternion.LookRotation(value);
            transform.forward = lookAt;
        }

        public bool IsWalking => _isWalking;

        
    }
}