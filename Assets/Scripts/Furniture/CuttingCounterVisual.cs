using UnityEngine;

namespace ZZOT.KitchenChaos.Furniture
{
    public class CuttingCounterVisual : MonoBehaviour
    {
        private const string CUT = "Cut";

        private Animator _animator;

        [SerializeField] private CuttingCounter _cuttingCounter;

        // Start is called before the first frame update
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _cuttingCounter.OnCut += CuttingCounter_OnCut;
        }

        private void CuttingCounter_OnCut(object sender, System.EventArgs e)
        {
            _animator.SetTrigger(CUT);
        }
    }
}
