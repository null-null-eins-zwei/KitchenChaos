using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.Character;

namespace ZZOT.KitchenChaos
{
    public class PlayerSound : MonoBehaviour
    {
        private Player _player;
        private float _footstepTimer;
        [SerializeField] private float _footstepTimerMax = 0.1f;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _footstepTimer = _footstepTimerMax;
        }

        private void Update()
        {
            _footstepTimer -= Time.deltaTime;
            if(_footstepTimer < 0)
            {
                _footstepTimer = _footstepTimerMax;
                if(_player.IsWalking)
                {
                    SoundManager.Instance.PlayFootstepSound(this.transform.position);
                }
            }
        }
    }
}
