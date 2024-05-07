using NaughtyAttributes;
using UnityEngine;

namespace TaskArcher.Scripts.Weapons
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletStartTransform;
        [SerializeField] private TargetingSystem targetingSystem;
        [SerializeField] private float forceFactor;
        [SerializeField] private float maxForce;
        [SerializeField] private float minForce;

        [InfoBox("Для наблюдения при дебаге, задавать не надо")]
        [SerializeField] private float force;
        public Transform BulletStartTransform => bulletStartTransform;
        public float Force => force;

        private void OnEnable()
        {
            onAttack += Shot;
        }

        private void OnDisable()
        {
            onAttack -= Shot;
        }

        private void Shot()
        {
            SpawnBullet(bulletStartTransform.position, bulletStartTransform.rotation);
        }

        private void SpawnBullet(Vector3 startPosition, Quaternion startRotation)
        {
            Bullet bullet = Instantiate(bulletPrefab, startPosition, startRotation);

            CalculateForce();
                
            bullet.onShotBullet?.Invoke(force);
        }

        public void CalculateForce()
        {
            var calcForce = forceFactor * targetingSystem.Distance.magnitude;

            if (calcForce > maxForce)
            {
                calcForce = maxForce;
            }
            
            if (calcForce < minForce)
            {
                calcForce = minForce;
            }

            force = calcForce;
        }
    }
}