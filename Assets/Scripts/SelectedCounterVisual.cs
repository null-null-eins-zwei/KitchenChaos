using UnityEngine;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class SelectedCounterVisual : MonoBehaviour
    {

        [SerializeField] private BaseCounter _counter;
        [SerializeField] private GameObject[] _visualGameObject;

        // Start is called before the first frame update
        void Start()
        {
            // Can't do this on Awake() because it is possible that this will be awaken earlier then Player 
            Player.Instance.OnSelectedConterChanged += Player_OnSelectedConterChanged;
        }

        private void Player_OnSelectedConterChanged(object sender, Player.OnSelectedConterChangedEventArgs e)
        {
            if (e.selectedCounter == _counter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show() => SetVisualsActive(true);

        private void Hide() => SetVisualsActive(false);

        private void SetVisualsActive(bool state)
        {
            foreach (var item in _visualGameObject)
            {
                item.SetActive(state);
            }
        }
    }
}
