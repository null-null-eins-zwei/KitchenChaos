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

        private int _cuttingProgress = 0;

        public override void Interact(Player player)
        {
            var playerItem = player.GetKitchenObject();

            if (playerItem != null)
            {
                if (!HasRecipeWithInput(playerItem.KitchenObjectSo))
                {
                    return;
                }
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
                
                _cuttingProgress = 0;
            }
        }

        public override void InteractAlternate(Player player)
        {
            if(HasKitchenObject())
            {
                _cuttingProgress++;


                var onTable = GetKitchenObject();

                if (!HasRecipeWithInput(onTable.KitchenObjectSo))
                {
                    return;
                }

                var recipe = GetRecipeForInput(onTable.KitchenObjectSo);

                if(recipe != null
                    && _cuttingProgress >= recipe.cuttingProgressMax)
                {
                    onTable.DestroySelf();
                    KitchenObject.SpawnKitchenObject(recipe.output, this);
                }
                
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO input) => GetRecipeForInput(input) != null;

        private CuttingRecipeSO GetRecipeForInput(KitchenObjectSO input)
        {
            foreach (var recipe in _allRecipes)
            {
                if (recipe.input == input)
                {
                    return recipe;
                }
            }

            return null;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
        {
            var recipe = GetRecipeForInput(input);

            if(recipe != null)
            {
                return recipe.output;
            }

            return null;
        }
    }
}
