using TaskArcher.Infrastructure.Services;
using UnityEngine;

namespace TaskArcher.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        T CreateBaseUIRoot<T>(T prefab) where T : Object;
    }
}