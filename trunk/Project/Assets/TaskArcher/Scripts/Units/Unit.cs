using TaskArcher.Infrastructure.Services.StaticData;
using TaskArcher.Weapons;
using UnityEngine;

namespace TaskArcher.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private Weapon weapon;
        [SerializeField] private Collider2D unitCollider;
        [SerializeField] private Rigidbody2D unitRigidbody;
        [SerializeField] private UnitDefinition unitDefinition;

        protected internal Weapon Weapon => weapon;
        protected internal Animator Animator => animator;

        private float _health;
        private float _lifeTimeAfterDeath;

        private void Start()
        {
            InitUnit();
        }
        private void InitUnit()
        {
            _health = unitDefinition.health;
            _lifeTimeAfterDeath = unitDefinition.lifeTimeAfterDeath;
        }

        protected void SetAnim(Animator unitAnimator, string clipName)
        {
            unitAnimator.CrossFade(clipName, 0f);
        }

        protected void TakeDamage(Unit unit, float damage)
        {
            unit._health -= damage;

            if (unit._health <= 0)
            {
                Death(unit.animator);
            }
        }

        private void Death(Animator unitAnimator)
        {
            SetAnim(unitAnimator, ConstsAnimNames.DeathClipName);

            unitCollider.enabled = false;
            unitRigidbody.simulated = false;
            
            Destroy(gameObject, _lifeTimeAfterDeath);
        }
    }
}