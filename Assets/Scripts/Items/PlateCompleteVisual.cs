using System;
using UnityEngine;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos.Items
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [Serializable]
        public struct KitchenObjectSo_GameObject
        {
            public KitchenObjectSO kitchenObjectSO;
            public GameObject gameObject;
        }

        [SerializeField] PlateKitchenObject _plate;
        [SerializeField] KitchenObjectSo_GameObject[] _plateCompleteObjectRelations;

        private void Start()
        {
            _plate.OnIngridientAdded += Plate_OnIngridientAdded;
        }

        private void Plate_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
        {
            foreach (var relation in _plateCompleteObjectRelations)
            {
                var onPlate = _plate.HasIngridient(relation.kitchenObjectSO);
                relation.gameObject.SetActive(onPlate);
            }
        }
    }
}
