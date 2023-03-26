using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZZOT.KitchenChaos.Scriptable;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private CuttingRecipeSO[] _allRecipes;

        public override void Interact(Player player)
        {
            var playerItem = player.GetKitchenObject();
            var receipeForPlayerItemExist = (playerItem != null)
                                            && HasRecipeWithInput(playerItem.KitchenObjectSo);
            if (!receipeForPlayerItemExist)
            {
                return;
            }

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
                var onTable = GetKitchenObject();

                if (!HasRecipeWithInput(onTable.KitchenObjectSo))
                {
                    return;
                }

                var output = GetOutputForInput(onTable.KitchenObjectSo);

                if(output != null)
                {
                    onTable.DestroySelf();
                    KitchenObject.SpawnKitchenObject(output, this);
                }
                
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO input) => _allRecipes.Any(r => r.input == input);

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
        {
            foreach (var recipe in _allRecipes)
            {
                if(recipe.input == input)
                {
                    return recipe.output;
                }
            }

            return null;
        }
    }
}
