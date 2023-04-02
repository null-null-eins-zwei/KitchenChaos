using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZZOT.KitchenChaos.Furniture
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] protected Transform _counterTopPoint;
        [SerializeField] protected Transform _platesVisualPrefab;
        [SerializeField] protected float _plateHeight = 0.1f;
        [SerializeField] protected PlatesCounter _platesCounter;

        private List<GameObject> _plates;

        private void Awake()
        {
            _plates = new();
        }

        private void Start()
        {
            _platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
            _platesCounter.OnPlateRemoved += PlatesCounter_OnPlateTaken;
        }

        private void PlatesCounter_OnPlateTaken(object sender, System.EventArgs e)
        {
            var lastPlate = _plates.LastOrDefault();
            if (lastPlate == null)
            {
                return;
            }

            _plates.Remove(lastPlate);
            Destroy(lastPlate);
        }

        private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
        {
            var plateVisualTransform = Instantiate(_platesVisualPrefab, _counterTopPoint);
            plateVisualTransform.position += Vector3.up * (_plates.Count * _plateHeight);

            _plates.Add(plateVisualTransform.gameObject);
        }
    }
}