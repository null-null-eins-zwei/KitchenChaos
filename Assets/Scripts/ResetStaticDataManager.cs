using UnityEngine;
using ZZOT.KitchenChaos.Furniture;

namespace ZZOT.KitchenChaos
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Awake()
        {
            BaseCounter.ResetStaticData();
            CuttingCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}
