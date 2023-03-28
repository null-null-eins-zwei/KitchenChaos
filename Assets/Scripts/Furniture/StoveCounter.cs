using System;
using UnityEngine;
using ZZOT.KitchenChaos.ScriptableObjects;
using ZZOT.KitchenChaos.User;
using static UnityEngine.EventSystems.EventTrigger;
using static ZZOT.KitchenChaos.Furniture.CuttingCounter;

namespace ZZOT.KitchenChaos.Furniture
{
    public class StoveCounter : BaseCounter
    {
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs: EventArgs
        {
            public StoveItemState state;
        }

        public enum StoveItemState
        {
            Idle = 0,
            Frying = 3,
            Fried = 7,
            Burned = 9,
        }

        [SerializeField] private FryingRecipeSO[] _allFryingRecipes;
        [SerializeField] private BurningRecipeSO[] _allBurningRecipes;


        private FryingRecipeSO _fryingRecipe;
        private BurningRecipeSO _burningRecipe;

        private StoveItemState _state;
        private StoveItemState State {
            get => _state;
            set 
            {
                _state = value;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = _state });
            }
        }

        // Coroutines also a good way to implement this
        private float _fryingTimer;
        private float _burningTimer;

        private void Start()
        {
            State = StoveItemState.Idle;
        }

        private void Update()
        {
            if (!HasKitchenObject())
            {
                return;
            }

            switch (State)
            {
                case StoveItemState.Idle:
                    break;
                case StoveItemState.Frying:
                    if (_fryingRecipe != null)
                    {
                        _fryingTimer += Time.deltaTime;

                        if (_fryingTimer > _fryingRecipe.fryingTimerSecMax)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(_fryingRecipe.output, this);

                            _burningTimer = 0;
                            State = StoveItemState.Fried;
                            _burningRecipe = GetBurningRecipeForInput(_fryingRecipe.output);
                        }
                    }

                    break;
                case StoveItemState.Fried:
                    _burningTimer += Time.deltaTime;

                    if (_burningTimer > _burningRecipe.burningTimerSecMax)
                    {
                        GetKitchenObject().DestroySelf(); 
                        KitchenObject.SpawnKitchenObject(_burningRecipe.output, this);

                        State = StoveItemState.Burned;
                    }

                    break; 
                case StoveItemState.Burned:
                    break;
                default:
                    break;
            }
        }

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

                _fryingRecipe = GetFryingRecipeForInput(playerItem.KitchenObjectSo);
                _burningRecipe = GetBurningRecipeForInput(playerItem.KitchenObjectSo);

                _fryingTimer = 0;
                _burningTimer = 0;

                State = StoveItemState.Frying;
                if (_burningRecipe != null)
                {
                    State = StoveItemState.Fried;
                }
            }
            else
            {
                _fryingTimer = 0;
                _burningTimer = 0;
                State = StoveItemState.Idle;
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO input) => GetFryingRecipeForInput(input) != null;

        private FryingRecipeSO GetFryingRecipeForInput(KitchenObjectSO input)
        {
            foreach (var recipe in _allFryingRecipes)
            {
                if (recipe.input == input)
                {
                    return recipe;
                }
            }

            return null;
        }

        private BurningRecipeSO GetBurningRecipeForInput(KitchenObjectSO input)
        {
            foreach (var recipe in _allBurningRecipes)
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
            var recipe = GetFryingRecipeForInput(input);

            if (recipe != null)
            {
                return recipe.output;
            }

            return null;
        }
    }
}
