using NaughtyAttributes;
using TaskArcher.Weapons;
using UnityEngine;

namespace TaskArcher.Infrastructure.Services.StaticData
{
	[CreateAssetMenu(fileName = "BulletDefinition", menuName = "StaticData/BulletDefinition", order = 0)]
	public class BulletDefinition : ScriptableObject
	{
		public Bullet bulletPrefab;
		public float bulletDamageMultiplier = 1f;
		public float lifeTimeWhenHitUnit;
		public float lifeTimeWhenHitObstacle;
		
		public bool needExplode;
		[InfoBox("Set if needExplode true:")]
		public float explosionRadius;
		public float explosionPower;
	}
}
