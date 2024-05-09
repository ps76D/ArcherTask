using System;
using UnityEngine;

namespace TaskArcher.Coins
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin coinPrefab;

        public static Action<Vector3> OnCoinSpawn;

        private void OnEnable()
        {
            OnCoinSpawn += SpawnCoin;
        }

        private void OnDisable()
        {
            OnCoinSpawn -= SpawnCoin;
        }

        private void SpawnCoin(Vector3 spawnPosition)
        {
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}