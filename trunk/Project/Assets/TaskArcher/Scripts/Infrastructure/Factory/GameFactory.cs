using TaskArcher.Infrastructure.AssetManagement;
using UnityEngine;

namespace TaskArcher.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        
        public T CreateBaseUIRoot<T>(T prefab) where T : Object
        {
            return Object.Instantiate(prefab);
        }
    }
}