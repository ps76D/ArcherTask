using System.Collections.Generic;
using UnityEngine;

namespace TaskArcher.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "StaticData/GameStaticData", order = 0)]
    public class GameStaticData : ScriptableObject
    {
        public List<BulletDefinition> allAmmoDefinitions;
    }
}