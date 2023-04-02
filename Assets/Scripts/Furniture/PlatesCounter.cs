using System;
using UnityEngine;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Items;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos.Furniture
{
    public class PlatesCounter : BaseCounter
    {
        private const float SPAWN_TIME = 2f;
        private const uint PLATES_MAX = 5;

        public event EventHandler OnPlateSpawn;
        public event EventHandler OnPlateRemoved;

        [SerializeField] private KitchenObjectSO _plateKitchenObjectSo;

        private float _spawnPlateTimer;
        private uint _platesSpawnAmount;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer > SPAWN_TIME)
            {
                _spawnPlateTimer = 0;
                if (_platesSpawnAmount < PLATES_MAX)
                {
                    _platesSpawnAmount++;
                    OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(Player player)
        {
            if (_platesSpawnAmount < 1)
            {
                return;
            }

            if (player.HasKitchenObject())
            {
                return;
            }

            _platesSpawnAmount--;
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);

            KitchenObject.SpawnKitchenObject(_plateKitchenObjectSo, player);
        }
    }
}