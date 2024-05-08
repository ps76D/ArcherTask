using System;
using Spine.Unity;
using TaskArcher.Scripts.Coins;
using UnityEngine;

namespace TaskArcher.Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation bulletSpine;
        [SerializeField] private AnimationReferenceAsset attack;
        [SerializeField] private Animator bulletParentAnimator;

        [SerializeField] private float lifeTimeWhenHitUnit;
        [SerializeField] private float lifeTimeWhenHitObstacle;
        [SerializeField] private int damage;
        
        [SerializeField] private TrailRenderer trail;
        
        [SerializeField] private LayerMask damageableLayerMask;
        [SerializeField] private LayerMask obstacleLayerMask;
        [SerializeField] private LayerMask dynamicObstacleLayerMask;

        [SerializeField] private Rigidbody2D bulletRigidbody;

        public Action<float> onShotBullet;
        
        private Vector3 _previousFramePosition;
        private bool _isShoot;
        private bool _isHit;
        private float _currentForce;
        
        private void OnEnable()
        {
            onShotBullet += Shoot;
            trail.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            onShotBullet -= Shoot;
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
            var direction = bulletRigidbody.velocity;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        private void PlayHitEffect()
        {
            bulletSpine.state.SetAnimation(0, attack, false);
        }

        private void HitEntity(Collider2D entity, Vector3 position)
        {
            _isHit = true;
            Units.Enemy getUnit = entity.GetComponent<Units.Enemy>();

            if (getUnit)
            {
                getUnit.onTakeDamage?.Invoke(getUnit, damage);
            }

            CoinSpawner.onCoinSpawn?.Invoke(position);
            CoinSpawner.onCoinSpawn?.Invoke(position);
            
            PlayHitEffect();
            
            transform.position = position;
            
            StopSimulation();
            Destroy(gameObject, lifeTimeWhenHitUnit);
        }
        
        private void HitObstacle(Vector3 position)
        {
            _isHit = true;
            transform.position = position;
            
            bulletParentAnimator.CrossFade("Hit", 0f);
            
            StopSimulation();
            Destroy(gameObject, lifeTimeWhenHitObstacle);
        }
        
        private void HitDynamicObstacle(Collider2D obstacle, Vector3 position)
        {
            _isHit = true;
            
            Rigidbody2D obstacleRigidbody = obstacle.GetComponent<Rigidbody2D>();
            obstacleRigidbody.AddForceAtPosition((Vector2)transform.right * _currentForce / 4, position, ForceMode2D.Impulse);
            
            /*CoinSpawner.onCoinSpawn?.Invoke(position);*/
            
            transform.position = position;
            transform.SetParent(obstacle.transform);
            
            bulletParentAnimator.CrossFade("Hit", 0f);
            
            StopSimulation();
            Destroy(gameObject, lifeTimeWhenHitObstacle);
        }

        private void StopSimulation()
        {
            bulletRigidbody.simulated = false;
            trail.gameObject.SetActive(false);
        }

        private void StateUpdate()
        {
            var lineEntity = Physics2D.Linecast(_previousFramePosition, 
                transform.position, damageableLayerMask);
            if (lineEntity && !lineEntity.collider.isTrigger)
            {
                HitEntity(lineEntity.collider, lineEntity.point);
            }
            
            var lineObstacle = Physics2D.Linecast(_previousFramePosition, 
                transform.position, obstacleLayerMask);
            if (lineObstacle && !lineObstacle.collider.isTrigger)
            {
                HitObstacle(lineObstacle.point);
            }
            
            var lineDynamicObstacle = Physics2D.Linecast(_previousFramePosition, 
                transform.position, dynamicObstacleLayerMask);
            if (lineDynamicObstacle && !lineDynamicObstacle.collider.isTrigger)
            {
                HitDynamicObstacle(lineDynamicObstacle.collider,lineDynamicObstacle.point);
            }

            _previousFramePosition = transform.position;
        }
    }
}