using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.ScriptableObjects;
using static ZZOT.KitchenChaos.PlateKitchenObject;

namespace ZZOT.KitchenChaos
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

        private void Plate_OnIngridientAdded(object sender, OnIngridientAddedEventArgs e)
        {
            foreach(var relation in _plateCompleteObjectRelations)
            {
                var onPlate = _plate.HasIngridient(relation.kitchenObjectSO);
                relation.gameObject.SetActive(onPlate);
            }
        }
    }
}
