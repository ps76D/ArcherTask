using UnityEngine;

namespace TaskArcher.Infrastructure.Services.StaticData
{
	[CreateAssetMenu(fileName = "RangeWeaponDefinition", menuName = "StaticData/RangeWeaponDefinition", order = 0)]
	public class RangeWeaponDefinition : WeaponDefinition
	{
		public float forceFactor;
		public float maxForce;
		public float minForce;
	}
}
