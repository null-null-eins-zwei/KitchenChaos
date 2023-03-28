using System.Linq;
using UnityEngine;
using static ZZOT.KitchenChaos.Furniture.StoveCounter;

namespace ZZOT.KitchenChaos.Furniture
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private StoveCounter _stoveCounter;
        [SerializeField] private GameObject _stoveOnVisual;
        [SerializeField] private GameObject _particles;

        private void Awake()
        {
        }

        // Start is called before the first frame update
        private void Start()
        {
            _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        }

        private void StoveCounter_OnStateChanged(object sender, OnStateChangedEventArgs e)
        {
            var activeStove = new[]
                                {
                                    StoveItemState.Frying,
                                    StoveItemState.Fried,
                                    StoveItemState.Burned,
                                }
                                .Contains(e.state);

            var activeParticles = new[]
                                {
                                    StoveItemState.Fried,
                                    StoveItemState.Burned,
                                }
                                .Contains(e.state);

            _stoveOnVisual.SetActive(activeStove);
            _particles.SetActive(activeParticles);
        }
    }
}
