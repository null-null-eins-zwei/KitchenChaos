using System;
using UnityEngine;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Items;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos.Furniture
{
    public class StoveCounter : BaseCounter, IHasProgress
    {
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

        public class OnStateChangedEventArgs : EventArgs
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
            set {
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

                        OnProgressChanged?.Invoke(
                            this,
                            new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = _fryingTimer / _fryingRecipe.fryingTimerSecMax,
                            });

                        if (_fryingTimer > _fryingRecipe.fryingTimerSecMax)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(_fryingRecipe.output, this);

                            _burningTimer = 0;
                            _burningRecipe = GetBurningRecipeForInput(_fryingRecipe.output);

                            State = StoveItemState.Fried;
                            OnProgressChanged?.Invoke(
                                this,
                                new IHasProgress.OnProgressChangedEventArgs
                                {
                                    progressNormalized = 1,
                                });
                        }
                    }

                    break;
                case StoveItemState.Fried:
                    _burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(
                            this,
                            new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = _burningTimer / _burningRecipe.burningTimerSecMax,
                            });

                    if (_burningTimer > _burningRecipe.burningTimerSecMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(_burningRecipe.output, this);

                        State = StoveItemState.Burned;
                        OnProgressChanged?.Invoke(
                            this,
                            new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = 1,
                            });
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
            var counterItem = this.GetKitchenObject();
            var counterHasItem = this.HasKitchenObject();
            var counterHasPlate = this.HasPlate();

            var playerItem = player.GetKitchenObject();
            var playerHasItem = player.HasKitchenObject();
            var playerHasPlate = player.HasPlate();

            if (playerHasItem
                && !playerHasPlate)
            {
                if (!HasRecipeWithInput(playerItem.KitchenObjectSo))
                {
                    return;
                }
            }

            if (counterHasItem)
            {
                if (playerHasPlate)
                {
                    var plate = playerItem as PlateKitchenObject;
                    if (plate.TryAddIngridient(counterItem.KitchenObjectSo))
                    {
                        counterItem.DestroySelf();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    player.ClearKitchenObject();
                    counterItem.SetKitchenObjectParent(player);
                }
            }

            if (playerHasItem
                && !playerHasPlate)
            {
                this.ClearKitchenObject();
                playerItem.SetKitchenObjectParent(this);

                _fryingRecipe = GetFryingRecipeForInput(playerItem.KitchenObjectSo);
                _burningRecipe = GetBurningRecipeForInput(playerItem.KitchenObjectSo);

                State = StoveItemState.Frying;
                if (_burningRecipe != null)
                {
                    State = StoveItemState.Fried;
                }
            }
            else
            {
                State = StoveItemState.Idle;
            }

            _fryingTimer = 0;
            _burningTimer = 0;

            OnProgressChanged?.Invoke(
                                this,
                                new IHasProgress.OnProgressChangedEventArgs
                                {
                                    progressNormalized = 0,
                                });
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
