using System.Collections.Generic;
using UnityEngine;

namespace ZZOT.KitchenChaos.ScriptableObjects
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public string recipeName;
        public List<KitchenObjectSO> recipeList;
    }
}
