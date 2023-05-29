using TMPro;
using UnityEngine;

namespace ZZOT.KitchenChaos
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        private const string TRIGGER_ANIMATION_POPUP = "NumberPopup";
        [SerializeField] private TextMeshProUGUI _countdownTMP;

        private Animator _animator;
        private int _lastCeilCount = 0;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

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

            var ceilCount = Mathf.CeilToInt(count);
            if (ceilCount != _lastCeilCount)
            {
                _lastCeilCount = ceilCount;
                _animator.SetTrigger(TRIGGER_ANIMATION_POPUP);
                SoundManager.Instance.PlayCountdownSound();
            }
        }

        private void Show() => gameObject.SetActive(true);

        private void Hide() => gameObject.SetActive(false);
    }
}
