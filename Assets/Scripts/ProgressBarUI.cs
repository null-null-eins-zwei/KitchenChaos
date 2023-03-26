using UnityEngine;
using UnityEngine.UI;
using ZZOT.KitchenChaos.Furniture;
using static ZZOT.KitchenChaos.Furniture.CuttingCounter;

namespace ZZOT.KitchenChaos
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] Image _barImage;
        [SerializeField] CuttingCounter _cuttingCounter;

        private void Start()
        {
            _cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
            SetProgress(0);
        }

        private void CuttingCounter_OnProgressChanged(object sender, OnProgressChangedEventArgs e) => SetProgress(e.progress);

        private void SetProgress(float value)
        {
            _barImage.fillAmount = value;
            gameObject.SetActive(value > 0);
        }
    }
}
