using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos
{
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recipeNameTMP;
        [SerializeField] private Transform _iconContainer;
        [SerializeField] private Transform _iconTemplate;

        private void Awake()
        {
            _iconTemplate.gameObject.SetActive(false);
        }

        public void SetRecipeSo(RecipeSO recipe)
        {
            _recipeNameTMP.text = recipe.recipeName;

            foreach(Transform icon in _iconContainer)
            {
                if(icon == _iconTemplate)
                {
                    continue;
                }

                Destroy(icon.gameObject);
            }

            foreach(var kitchenObjectSo in recipe.recipeList)
            {
                var newIcon = Instantiate(_iconTemplate, parent: _iconContainer);
                newIcon.GetComponent<Image>().sprite = kitchenObjectSo.sprite;
                newIcon.gameObject.SetActive(true);
            }
        }
    }
}
