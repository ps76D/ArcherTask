using UnityEngine;

namespace TaskArcher.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject InstantiateByPath(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject InstantiateByPath(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}