using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos
{
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;
        public class OnIngridientAddedEventArgs: EventArgs
        {
            public KitchenObjectSO kitchenObjectSo;
        }

        [SerializeField] private List<KitchenObjectSO> _validIngridients;

        private List<KitchenObjectSO> _kitchenObjectSoList;

        private void Awake()
        {
            _kitchenObjectSoList = new();
        }

        public bool TryAddIngridient(KitchenObjectSO kitchenObjectSo)
        {
            if (!CanAddIngridient(kitchenObjectSo))
            {
                return false;
            }

            _kitchenObjectSoList.Add(kitchenObjectSo);
            OnIngridientAdded?.Invoke(
                this, 
                new OnIngridientAddedEventArgs
                {
                    kitchenObjectSo = kitchenObjectSo,
                });

            return true;
        }

        public bool HasIngridient(KitchenObjectSO kitchenObjectSo) =>
            _kitchenObjectSoList.Contains(kitchenObjectSo);

        private bool CanAddIngridient(KitchenObjectSO kitchenObjectSo)
        {
            if (!_validIngridients.Contains(kitchenObjectSo))
            {
                return false;
            }

            if (_kitchenObjectSoList.Contains(kitchenObjectSo))
            {
                return false;
            }

            return true;
        }
    }
}
