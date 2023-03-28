using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using ZZOT.KitchenChaos.ScriptableObjects;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class CuttingCounter : BaseCounter
    {
        public event EventHandler OnCut;
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs 
        { 
            public float progress;
        }

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

                _cuttingProgress = 0;
                OnProgressChanged?.Invoke(
                    this,
                    new OnProgressChangedEventArgs()
                    {
                        progress = 0
                    });
            }

            if (playerItem != null)
            {
                this.ClearKitchenObject();
                playerItem.SetKitchenObjectParent(this);
                
                _cuttingProgress = 0;
                OnProgressChanged?.Invoke(
                    this,
                    new OnProgressChangedEventArgs()
                    {
                        progress = 0
                    });
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

                var recipe = GetRecipeForInput(onTable.KitchenObjectSo);

                _cuttingProgress++;

                if(_cuttingProgress >= recipe.cuttingProgressMax)
                {
                    onTable.DestroySelf();
                    KitchenObject.SpawnKitchenObject(recipe.output, this);

                    OnProgressChanged?.Invoke(
                        this,
                        new OnProgressChangedEventArgs() { progress = 0 });
                }
                else
                {
                    OnProgressChanged?.Invoke(
                        this,
                        new OnProgressChangedEventArgs()
                        {
                            progress = (float)_cuttingProgress / recipe.cuttingProgressMax,
                        });

                    OnCut?.Invoke(this, EventArgs.Empty);
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
