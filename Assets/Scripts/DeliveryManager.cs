using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ZZOT.KitchenChaos.Items;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos
{
    public class DeliveryManager : MonoBehaviour
    {
        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFailed;

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

            if (_spawnRecipeTimer > -1f)
            { 
                _spawnRecipeTimer -= Time.deltaTime;
            }
        
            if (_spawnRecipeTimer <= 0f
                && KitchenGameManager.Instance.IsGamePlaying)
            {
                _spawnRecipeTimer = _spawnRecipeTimerMax;

                var randomRecipe = _allowedRecipes.GetRandom();
                _waitingRecipeSoList.Add(randomRecipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }

        public RecipeSO FindWaitedRecipeOnPlate(PlateKitchenObject plate)
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
            var recipeOnPlate = FindWaitedRecipeOnPlate(plate);
            if (recipeOnPlate == null)
            {
                OnRecipeFailed?.Invoke(this, EventArgs.Empty);
                return false;
            }


            // take Plate from Player
            // remove from waiting
            _waitingRecipeSoList.Remove(recipeOnPlate);
            plate.DestroySelf();

            OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
            OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public IList<RecipeSO> WaitingRecipeSoList => _waitingRecipeSoList.AsReadOnlyList();
    }
}
