using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.Scriptable;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO _cutKitchenObject;

        public override void Interact(Player player)
        {
            var playerItem = player.GetKitchenObject();
            var counterItem = this.GetKitchenObject();

            if (counterItem != null)
            {
                player.ClearKitchenObject();
                counterItem.SetKitchenObjectParent(player);
            }

            if (playerItem != null)
            {
                this.ClearKitchenObject();
                playerItem.SetKitchenObjectParent(this);
            }
        }

        public override void InteractAlternate(Player player)
        {
            if(HasKitchenObject())
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(_cutKitchenObject, this);
            }
        }
    }
}
