using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Items;

namespace ZZOT.KitchenChaos.Furniture
{
    public class DeliveryCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (player.HasPlate())
            {
                var plate = player.GetKitchenObject() as PlateKitchenObject;
                if (DeliveryManager.Instance.TryDeliveryRecipe(plate))
                {
                    // score?
                }
            }
        }
    }
}