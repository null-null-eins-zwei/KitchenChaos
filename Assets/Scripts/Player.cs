using UnityEngine;

namespace ZZOT.KitchenChaos
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float playesSpeed = 7f;

        // Update is called once per frame
        private void Update()
        {
            Vector2 input = new();

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

            input = playesSpeed * Time.deltaTime * input.normalized;

            Vector3 move = new(
                            x: input.x,
                            y: 0f,
                            z: input.y);


            transform.position += move;

            float rotateSpeed = 10f;
            Vector3 lookAt = Vector3.Slerp(
                                transform.forward, 
                                move,
                                Time.deltaTime * rotateSpeed);

            //transform.LookAt();
            transform.forward = lookAt;
        }
    }
}