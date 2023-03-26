using UnityEngine;

namespace ZZOT.KitchenChaos
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt = 0,
            LookAtInverted = 1,

            CameraForward = 5,
            CameraForwardInverted = 6,
        }

        [SerializeField] private Mode _mode;

        private void LateUpdate()
        {
            Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
            Vector3 dirToCamera = dirFromCamera * -1;

            switch (_mode)
            {
                case Mode.LookAtInverted:
                    transform.LookAt(transform.position + dirFromCamera);
                    break;

                case Mode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;

                case Mode.CameraForwardInverted:
                    transform.forward = Camera.main.transform.forward * -1;
                    break;

                default:
                    transform.LookAt(Camera.main.transform);
                    break;
            }
        }
    }
}
