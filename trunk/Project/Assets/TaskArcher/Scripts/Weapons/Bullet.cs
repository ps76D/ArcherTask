using System;
using NaughtyAttributes;
using Spine.Unity;
using TaskArcher.Coins;
using TaskArcher.Infrastructure.Services.StaticData;
using TaskArcher.Units;
using UnityEngine;

namespace TaskArcher.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletDefinition bulletDefinition;
        
        [SerializeField] private SkeletonAnimation bulletSpine;
        [SerializeField] private AnimationReferenceAsset attack;
        [SerializeField] private Animator bulletParentAnimator;

        private float _lifeTimeWhenHitUnit;
        private float _lifeTimeWhenHitObstacle;

        [SerializeField] private TrailRenderer trail;

        [SerializeField] private LayerMask damageableLayerMask;
        [SerializeField] private LayerMask obstacleLayerMask;
        [SerializeField] private LayerMask dynamicObstacleLayerMask;

        [SerializeField] private Rigidbody2D bulletRigidbody;
        
        [InfoBox("Set if needExplode true:")]
        [SerializeField] private Explosion explosion;

        public Action<float> OnShotBullet;

        private Action<RaycastHit2D> _onHitEntity;
        private Action<RaycastHit2D> _onHitObstacle;
        private Action<RaycastHit2D> _onHitDynamicObstacle;

        public float Damage {
            get;
            set;
        }

        private Vector3 _previousFramePosition;
        private bool _isShoot;
        private bool _isHit;
        private float _currentForce;
        
        private void OnEnable()
        {
            OnShotBullet += Shoot;
            _onHitEntity += HitEntity;
            _onHitObstacle += HitObstacle;
            _onHitDynamicObstacle += HitDynamicObstacle;
            
            trail.gameObject.SetActive(false);

            _lifeTimeWhenHitUnit = bulletDefinition.lifeTimeWhenHitUnit;
            _lifeTimeWhenHitObstacle = bulletDefinition.lifeTimeWhenHitObstacle;
        }

        private void OnDestroy()
        {
            OnShotBullet -= Shoot;
            _onHitEntity -= HitEntity;
            _onHitObstacle -= HitObstacle;
            _onHitDynamicObstacle -= HitDynamicObstacle;
        }

        private void Update()
        {
            Rotate();
        }

        private void FixedUpdate()
        {
            if (!_isHit)
            {
                StateUpdate();
            }
        }

        private void Shoot(float force)
        {
            _currentForce = force;
            bulletRigidbody.simulated = true;
            bulletRigidbody.AddForce((Vector2)transform.right * force, ForceMode2D.Impulse);

            _isShoot = true;
            _previousFramePosition = transform.position;
            
            trail.gameObject.SetActive(true);
        }

        private void Rotate()
        {
            if (!_isShoot) return;
            Vector2 direction = bulletRigidbody.velocity;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        private void PlayHitEffect()
        {
            bulletSpine.state.SetAnimation(0, attack, false);
        }

        private void HitEntity(RaycastHit2D raycastHit)
        {
            _isHit = true;
            Enemy getUnit = raycastHit.collider.GetComponent<Enemy>();

            if (getUnit)
            {
                getUnit.OnTakeDamage?.Invoke(getUnit, Damage);
            }

            CoinSpawner.OnCoinSpawn?.Invoke(raycastHit.point);
            CoinSpawner.OnCoinSpawn?.Invoke(raycastHit.point);
            
            PlayHitEffect();
            
            transform.position = raycastHit.point;
            
            CheckIfNeedExplosion(raycastHit);
            
            StopSimulation();
            Destroy(gameObject, _lifeTimeWhenHitUnit);
        }
        
        private void HitObstacle(RaycastHit2D raycastHit)
        {
            _isHit = true;
            transform.position = raycastHit.point;
            
            bulletParentAnimator.CrossFade(ConstsAnimNames.Hit, 0f);
           
            CheckIfNeedExplosion(raycastHit);
            
            StopSimulation();
            Destroy(gameObject, _lifeTimeWhenHitObstacle);
        }
        
        private void HitDynamicObstacle(RaycastHit2D raycastHit)
        {
            _isHit = true;
            
            Rigidbody2D obstacleRigidbody = raycastHit.collider.GetComponent<Rigidbody2D>();
            obstacleRigidbody.AddForceAtPosition((Vector2)transform.right * _currentForce / 4, 
                raycastHit.point, ForceMode2D.Impulse);
            
            transform.position = raycastHit.point;
            transform.SetParent(raycastHit.transform);
            
            bulletParentAnimator.CrossFade(ConstsAnimNames.Hit, 0f);

            CheckIfNeedExplosion(raycastHit);
            
            StopSimulation();
            Destroy(gameObject, _lifeTimeWhenHitObstacle);
        }

        private void StopSimulation()
        {
            bulletRigidbody.simulated = false;
            trail.gameObject.SetActive(false);
        }

        private void StateUpdate()
        {
            CheckCollisionHit(damageableLayerMask, _onHitEntity.Invoke);
            CheckCollisionHit(obstacleLayerMask, _onHitObstacle.Invoke);
            CheckCollisionHit(dynamicObstacleLayerMask, _onHitDynamicObstacle.Invoke);

            _previousFramePosition = transform.position;
        }

        private void CheckCollisionHit(LayerMask layerMask, Action<RaycastHit2D> onHit)
        {
            RaycastHit2D raycastHit = Physics2D.Linecast(_previousFramePosition, 
                transform.position, layerMask);
            if (raycastHit && !raycastHit.collider.isTrigger)
            {
                onHit?.Invoke(raycastHit);
            }
        }

        private void CheckIfNeedExplosion(RaycastHit2D raycastHit2D)
        {
            if (!bulletDefinition.needExplode) return;
            
            if (explosion != null) 
            {
                explosion.Explode(raycastHit2D, bulletDefinition.explosionRadius, bulletDefinition.explosionPower);
            }
        }
    }
}