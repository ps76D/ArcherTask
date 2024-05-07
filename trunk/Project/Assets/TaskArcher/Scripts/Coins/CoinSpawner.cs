using System;
using UnityEngine;

namespace TaskArcher.Scripts.Coins
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin coinPrefab;

        public static Action<Vector3> onCoinSpawn;

        private void OnEnable()
        {
            onCoinSpawn += SpawnCoin;
        }

        private void OnDisable()
        {
            onCoinSpawn -= SpawnCoin;
        }

        private void SpawnCoin(Vector3 spawnPosition)
        {
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}