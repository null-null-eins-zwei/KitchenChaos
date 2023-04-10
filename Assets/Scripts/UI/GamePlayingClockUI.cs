using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZZOT.KitchenChaos
{
    public class GamePlayingClockUI : MonoBehaviour
    {
        [SerializeField] private Image _timerImage;

        // Update is called once per frame
        void Update()
        {
            var isPlaying = KitchenGameManager.Instance.IsGamePlaying;
            if (!isPlaying) 
            {
                _timerImage.fillAmount = 0;
                return;
            }

            _timerImage.fillAmount = KitchenGameManager.Instance.GetPlayingTimerNormalized();
        }
    }
}
