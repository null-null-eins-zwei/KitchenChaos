using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.Items;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance {
            get;
            private set;
        }

        [SerializeField] private RecipeListSO _allowedRecipes;

        private List<RecipeSO> _waitingRecipeSoList;

        private int _waitingRecipesCountMax = 4;
        private float _spawnRecipeTimerMax = 4f;
        private float _spawnRecipeTimer;

        private void Awake()
        {
            _waitingRecipeSoList = new List<RecipeSO>();
            Instance = this;
        }

        private void Update()
        {
            if (_waitingRecipeSoList.Count >= _waitingRecipesCountMax)
            {
                _spawnRecipeTimer = _spawnRecipeTimerMax;
                return;
            }

            _spawnRecipeTimer -= Time.deltaTime;
            if (_spawnRecipeTimer <= 0f)
            {
                _spawnRecipeTimer = _spawnRecipeTimerMax;

                var randomRecipe = _allowedRecipes.GetRandom();
                _waitingRecipeSoList.Add(randomRecipe);
            }
        }

        public RecipeSO WaitedRecipeOnPlate(PlateKitchenObject plate)
        {
            var plateList = plate.GetKitchenObjectSoList();

            foreach (var waitingRecipe in _waitingRecipeSoList)
            {
                if (waitingRecipe.recipeList.Count != plateList.Count)
                {
                    continue;
                }

                var recipeMatch = true;
                foreach (var recipeIngridient in waitingRecipe.recipeList)
                {
                    var ingridientMatch = false;
                    foreach (var ingridientOnPlate in plateList)
                    {
                        if (recipeIngridient == ingridientOnPlate)
                        {
                            // Recipe ingridient found on Plate
                            ingridientMatch = true;
                            break;
                        }
                    }

                    if (!ingridientMatch)
                    {
                        // at least one ingridient doesn't match recipe
                        recipeMatch = false;
                    }
                }

                if (recipeMatch)
                {
                    return waitingRecipe;
                }
            }

            return null;
        }

        public bool TryDeliveryRecipe(PlateKitchenObject plate)
        {
            var recipeOnPlate = WaitedRecipeOnPlate(plate);
            if (recipeOnPlate == null)
            {
                return false;
            }


            // take Plate from Player
            // remove from waiting
            _waitingRecipeSoList.Remove(recipeOnPlate);
            plate.DestroySelf();

            // Score

            return true;
        }
    }
}
