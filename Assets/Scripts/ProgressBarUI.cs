using UnityEngine;
using UnityEngine.UI;
using ZZOT.KitchenChaos.Furniture;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static ZZOT.KitchenChaos.Furniture.CuttingCounter;

namespace ZZOT.KitchenChaos
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private Image _barImage;
        [SerializeField] private GameObject _hasProgressGameObject;
        private IHasProgress _counter;

        private void Start()
        {
            if (!_hasProgressGameObject.TryGetComponent<IHasProgress>(out _counter))
            {
                Debug.LogError($"{_counter} has no {nameof(IHasProgress)} interface.");
            }
            
            _counter.OnProgressChanged += Sender_OnProgressChanged;
            SetProgress(0);
        }

        private void Sender_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) =>
            SetProgress(e.progressNormalized);

        private void SetProgress(float value)
        {
            _barImage.fillAmount = value;
            gameObject.SetActive(value > 0);
        }
    }
}
