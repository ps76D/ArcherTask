using UnityEngine;

namespace TaskArcher.Infrastructure.Services.StaticData
{
	[CreateAssetMenu(fileName = "UnitDefinition", menuName = "StaticData/UnitDefinition", order = 0)]
	public class UnitDefinition : ScriptableObject
	{
		public float health;
		public float lifeTimeAfterDeath;
	}
}
