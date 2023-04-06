using System.Collections.Generic;
using UnityEngine;

namespace ZZOT.KitchenChaos.ScriptableObjects
{
    //[CreateAssetMenu()] - commented after i create one object of this
    public class RecipeListSO : ScriptableObject
    {
        public List<RecipeSO> recipts;
    }

    public static class RecipeListSOExtensions
    {
        public static RecipeSO GetRandom(this RecipeListSO recipeList)
        {
            var count = recipeList.recipts.Count;
            if(count == 0)
            {
                return null;
            }

            var ir = Random.Range(0, recipeList.recipts.Count);

            return recipeList.recipts[ir];
        }
    }
}
