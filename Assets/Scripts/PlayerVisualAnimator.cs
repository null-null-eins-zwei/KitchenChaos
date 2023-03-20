using UnityEngine;

namespace ZZOT.KitchenChaos.User
{
    public class PlayerVisualAnimator : MonoBehaviour
    {
        private const string IS_WALKING = "IsWalking";
        private Animator _animator;

        [SerializeField] private Player _player;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(IS_WALKING, _player.IsWalking);
        }
    }
}
