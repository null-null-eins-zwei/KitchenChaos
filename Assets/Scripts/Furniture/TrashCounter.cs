using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}