using UnityEngine;

namespace ZZOT.KitchenChaos.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 7f;

        private float _rotateSpeed = 10f;
        private bool _isWalking = false;

        // Update is called once per frame
        private void Update()
        {
            Vector2 input = GetInput() * _playerSpeed;

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

        private Vector2 GetInput()
        {
            Vector2 input = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                input += Vector2.up; // y+1
            }

            if (Input.GetKey(KeyCode.S))
            {
                input += Vector2.down; // y-1
            }

            if (Input.GetKey(KeyCode.A))
            {
                input += Vector2.left; // x-1
            }

            if (Input.GetKey(KeyCode.D))
            {
                input += Vector2.right; // x+1
            }

            // we need normalize because of diagonal cases,
            // Deltatime required because of variable framerate
            return input.normalized * Time.deltaTime;
        }
    }
}