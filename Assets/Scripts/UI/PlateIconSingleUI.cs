using UnityEngine;
using UnityEngine.UI;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos.UI

{
    public class PlateIconSingleUI : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;

        public void SetKitchenObjectSo(KitchenObjectSO so)
        {
            _iconImage.sprite = so.sprite;
        }
    }
}
