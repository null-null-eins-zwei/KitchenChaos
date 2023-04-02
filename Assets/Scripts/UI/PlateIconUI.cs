using UnityEngine;
using ZZOT.KitchenChaos.Items;

namespace ZZOT.KitchenChaos.UI

{
    public class PlateIconUI : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject _plate;
        [SerializeField] private Transform _iconTemplate;

        private void Start()
        {
            _iconTemplate.gameObject.SetActive(false);

            _plate.OnIngridientAdded += Plate_OnIngridientAdded;
        }

        private void Plate_OnIngridientAdded(
            object sender,
            PlateKitchenObject.OnIngridientAddedEventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in transform)
            {
                if (child == _iconTemplate)
                {
                    continue;
                }

                Destroy(child.gameObject);
            }

            foreach (var so in _plate.GetKitchenObjectSoList())
            {
                var iconTransform = Instantiate(_iconTemplate, parent: transform);
                iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSo(so);
                iconTransform.gameObject.SetActive(true);
            }
        }
    }
}
