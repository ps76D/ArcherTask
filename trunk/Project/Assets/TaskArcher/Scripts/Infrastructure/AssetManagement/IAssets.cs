using TaskArcher.Infrastructure.Services;
using UnityEngine;

namespace TaskArcher.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject InstantiateByPath(string path);
        GameObject InstantiateByPath(string path, Vector3 at);
    }
}