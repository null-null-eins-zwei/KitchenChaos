using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

namespace ZZOT.KitchenChaos
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _countdownTMP;

        private void Start()
        {
            KitchenGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Hide();
        }

        private void GameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if (KitchenGameManager.Instance.IsCountdownToStartActive)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Update()
        {
            var count = KitchenGameManager.Instance.GetCountdownToStartTimer();
            _countdownTMP.text = count.ToString("F2");
        }

        private void Show() => gameObject.SetActive(true);

        private void Hide() => gameObject.SetActive(false);        
    }
}
