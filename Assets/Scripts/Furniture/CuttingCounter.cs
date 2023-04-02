using System;
using UnityEngine;
using ZZOT.KitchenChaos.Interfaces;
using ZZOT.KitchenChaos.ScriptableObjects;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        public event EventHandler OnCut;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

        [SerializeField] private CuttingRecipeSO[] _allRecipes;

        private int _cuttingProgress = 0;

        public override void Interact(Player player)
        {
            var counterItem = this.GetKitchenObject();
            var counterHasItem = this.HasKitchenObject();
            var counterHasPlate = this.HasPlate();

            var playerItem = player.GetKitchenObject();
            var playerHasItem = player.HasKitchenObject();
            var playerHasPlate = player.HasPlate();

            if (playerHasPlate
                    && counterHasItem
                    && !counterHasPlate)
            {
                var plate = playerItem as PlateKitchenObject;

                if (plate.TryAddIngridient(counterItem.KitchenObjectSo))
                {
                    counterItem.DestroySelf();
                }
            }
            else
            {
                if (playerHasItem)
                {
                    if (!HasRecipeWithInput(playerItem.KitchenObjectSo))
                    {
                        return;
                    }
                }


                if (counterHasItem)
                {
                    player.ClearKitchenObject();
                    counterItem.SetKitchenObjectParent(player);

                    _cuttingProgress = 0;
                    OnProgressChanged?.Invoke(
                        this,
                        new IHasProgress.OnProgressChangedEventArgs() { progressNormalized = 0 });
                }

                if (playerHasItem)
                {
                    this.ClearKitchenObject();
                    playerItem.SetKitchenObjectParent(this);

                    _cuttingProgress = 0;
                    OnProgressChanged?.Invoke(
                        this,
                        new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = 0,
                        });
                }
            }
        }

        public override void InteractAlternate(Player player)
        {
            if (!HasKitchenObject())
            {
                return;
            }

            var onTable = GetKitchenObject();
            if (!HasRecipeWithInput(onTable.KitchenObjectSo))
            {
                return;
            }

            var recipe = GetRecipeForInput(onTable.KitchenObjectSo);

            _cuttingProgress++;

            if (_cuttingProgress >= recipe.cuttingProgressMax)
            {
                onTable.DestroySelf();
                KitchenObject.SpawnKitchenObject(recipe.output, this);

                OnProgressChanged?.Invoke(
                    this,
                    new IHasProgress.OnProgressChangedEventArgs() { progressNormalized = 0 });
            }
            else
            {
                OnProgressChanged?.Invoke(
                    this,
                    new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = (float)_cuttingProgress / recipe.cuttingProgressMax,
                    });

                OnCut?.Invoke(this, EventArgs.Empty);
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

            if (recipe != null)
            {
                return recipe.output;
            }

            return null;
        }
    }
}
