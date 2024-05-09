using System;
using NaughtyAttributes;
using TaskArcher.Infrastructure.Services.StaticData;
using UnityEngine;

namespace TaskArcher.Weapons
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private Transform bulletStartTransform;
        [SerializeField] private TargetingSystem targetingSystem;

        [SerializeField] private RangeWeaponDefinition weaponDefinition;
        [SerializeField] private BulletDefinition currentBulletDefinition;

        [InfoBox("Для наблюдения при дебаге, задавать не надо")]
        [SerializeField] private float force;
        
        public Transform BulletStartTransform => bulletStartTransform;
        public BulletDefinition CurrentBulletDefinition {
            get => currentBulletDefinition;
            set => currentBulletDefinition = value;
        }
        public float Force => force;
        
        private float _forceFactor;
        private float _maxForce;
        private float _minForce;

        private StaticData _staticData;
        
        private void OnEnable()
        {
            OnAttack += Shot;
            
            InitWeapon();
        }

        private void InitWeapon()
        {
            _staticData = StaticData.Instance;
            
            _forceFactor = weaponDefinition.forceFactor;
            _maxForce = weaponDefinition.maxForce;
            _minForce = weaponDefinition.minForce;
            
            CurrentBulletDefinition = _staticData.GameStaticData.allAmmoDefinitions[0];
        }

        private void OnDisable()
        {
            OnAttack -= Shot;
        }

        private void Shot()
        {
            SpawnBullet(bulletStartTransform.position, bulletStartTransform.rotation);
        }

        private void SpawnBullet(Vector3 startPosition, Quaternion startRotation)
        {
            Bullet bullet = Instantiate(currentBulletDefinition.bulletPrefab, startPosition, startRotation);
            bullet.Damage = weaponDefinition.damage * currentBulletDefinition.bulletDamageMultiplier;
            CalculateForce();
                
            bullet.OnShotBullet?.Invoke(force);
        }

        public void CalculateForce()
        {
            var calcForce = _forceFactor * targetingSystem.Distance.magnitude;

            if (calcForce > _maxForce)
            {
                calcForce = _maxForce;
            }
            
            if (calcForce < _minForce)
            {
                calcForce = _minForce;
            }

            force = calcForce;
        }
    }
}