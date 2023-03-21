using UnityEngine;

namespace ZZOT.KitchenChaos.Furniture
{
    public class ContainerCounterVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";

        private Animator _animator;

        [SerializeField]
        private ContainerCounter _containerCounter;

        // Start is called before the first frame update
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
        }

        private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
        {
            _animator.SetTrigger(OPEN_CLOSE);
        }
    }
}
