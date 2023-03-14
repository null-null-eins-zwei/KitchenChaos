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

        private readonly float _playerSize = 0.5f;
        private readonly float _playerHeight = 2f;


        // Update is called once per frame
        private void Update()
        {

            Vector2 input = _input.GetMovementVectorNormalized();

            Vector3 moveDir = new(
                            x: input.x,
                            y: 0f,
                            z: input.y);


            float moveDistance = _playerSpeed * Time.deltaTime;

            var canMove = CanMove(moveDir, moveDistance);

            // we want to slide toward the wall in case if we hit it
            if (!canMove)
            {
                // Attempt only X dir
                Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

                var canMoveX = (moveDirX != Vector3.zero) && CanMove(moveDirX, moveDistance);
                var canMoveZ = (moveDirZ != Vector3.zero) && CanMove(moveDirZ, moveDistance);

                if (canMoveX)
                {
                    canMove = true;
                    moveDir = moveDirX;
                }
                else if (canMoveZ)
                {
                    canMove = true;
                    moveDir = moveDirZ;
                }
            }

            _isWalking = moveDir != Vector3.zero;

            if (canMove)
            {
                // deltaTime required because of variable framerate
                transform.position += moveDistance * moveDir;
            }


            Vector3 lookAt = Vector3.Slerp(
                                 transform.forward,
                                 moveDir,
                                 Time.deltaTime * _rotateSpeed); // this is strange, there is better ways to determine rotation speed

            //rotation = Quaternion.LookRotation(value);
            transform.forward = lookAt;
        }

        public bool IsWalking => _isWalking;

        private bool CanMove(Vector3 moveDir, float moveDistance)
        {
            var hit = Physics.CapsuleCast(
                        point1: transform.position,
                        point2: transform.position + Vector3.up * _playerHeight,
                        radius: _playerSize,
                        direction: moveDir,
                        maxDistance: moveDistance);

            return !hit;
        }
    }
}