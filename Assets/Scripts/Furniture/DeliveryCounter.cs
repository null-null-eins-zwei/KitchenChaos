using System;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Items;

namespace ZZOT.KitchenChaos.Furniture
{
    public class DeliveryCounter : BaseCounter
    {
        public static DeliveryCounter Instance { get; private set; }

        public event EventHandler OnRecipeSuccessfulyDelivered;

        private void Awake()
        {
            Instance = this;
        }

        public override void Interact(Player player)
        {
            if (player.HasPlate())
            {
                var plate = player.GetKitchenObject() as PlateKitchenObject;
                if (DeliveryManager.Instance.TryDeliveryRecipe(plate))
                {
                    OnRecipeSuccessfulyDelivered?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}