using UnityEngine;

namespace TaskArcher.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper bootstrapperPrefab;
        private void Awake()
        {
            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();
            if (bootstrapper == null)
                Instantiate(bootstrapperPrefab);
        }
    }
}