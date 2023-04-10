using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZZOT.KitchenChaos
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _deliveredCountText;

        private void Start()
        {
            KitchenGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Hide();
        }

        private void GameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if (KitchenGameManager.Instance.IsGameOver)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            var count = KitchenGameManager.Instance.GetDeliveredRecipesCount();
            _deliveredCountText.text = count.ToString();

            gameObject.SetActive(true);
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
