using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.Furniture;

namespace ZZOT.KitchenChaos
{
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveCounter _counter;
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _counter.OnStateChanged += Counter_OnStateChanged;
        }

        private void Counter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
        {
            var playSound = e.state == StoveCounter.StoveItemState.Frying
                            || e.state == StoveCounter.StoveItemState.Fried;
            if(playSound)
            {
                _source.Play();
            }
            else
            {
                _source.Pause();
            }
        }
    }
}
