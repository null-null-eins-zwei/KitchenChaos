using UnityEngine;
using ZZOT.KitchenChaos.Scriptable;

namespace ZZOT.KitchenChaos.Furniture
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private Transform _counterTopPoint;
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;

        public void Interact()
        {
            Debug.Log("Interact!");
            var kitchenObjectTransform = Instantiate(
                                    original: _kitchenObjectSO.prefab,
                                    parent: _counterTopPoint);

            kitchenObjectTransform.localPosition = Vector3.zero;

            var scriptableObject = kitchenObjectTransform.GetComponent<KitchenObject>().ScriptableObject;

            Debug.Log(scriptableObject);
        }
    }
}