using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace ZZOT.KitchenChaos
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _recipeTemplate;

        private void Awake()
        {
            _recipeTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        }

        private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
        {
            UpdateVisual();
        }

        private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach(Transform child in _container)
            {
                if(child == _recipeTemplate)
                {
                    continue;
                }
                Destroy(child.gameObject);
            }

            foreach(var recipe in DeliveryManager.Instance.WaitingRecipeSoList)
            {
                var newItem = Instantiate(_recipeTemplate, parent: _container);
                newItem.gameObject.SetActive(true);

                newItem.GetComponent<DeliveryManagerSingleUI>()
                    .SetRecipeSo(recipe);
            }
        }
    }
}
