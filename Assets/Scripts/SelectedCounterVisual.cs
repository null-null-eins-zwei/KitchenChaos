using UnityEngine;

namespace ZZOT.KitchenChaos.Furniture
{
    public class SelectedCounterVisual : MonoBehaviour
    {

        [SerializeField] private ClearCounter _clearCounter;
        [SerializeField] private GameObject _visualGameObject;

        // Start is called before the first frame update
        void Start()
        {
            // Can't do this on Awake() because it is possible that this will be awaken earlier then Player 
            User.Player.Instance.OnSelectedConterChanged += Player_OnSelectedConterChanged;
        }

        private void Player_OnSelectedConterChanged(object sender, User.Player.OnSelectedConterChangedEventArgs e)
        {
            if(e.selectedCounter == _clearCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            _visualGameObject.SetActive(true);
        }

        private void Hide()
        {
            _visualGameObject.SetActive(false);
        }
    }
}
