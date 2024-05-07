using TaskArcher.Scripts.Weapons;
using Test.Scripts;
using UnityEngine;

namespace TaskArcher.Scripts.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private int health;
        [SerializeField] private Weapon weapon;
        [SerializeField] private Collider2D unitCollider;
        [SerializeField] private float lifeTimeAfterDeath;
        [SerializeField] private Rigidbody2D unitRigidbody;
        
        protected internal Weapon Weapon => weapon;
        protected internal Animator Animator => animator;
        
        protected void SetAnim(Animator unitAnimator, string clipName)
        {
            unitAnimator.CrossFade(clipName, 0f);
        }

        protected void TakeDamage(Unit unit, int damage)
        {
            unit.health -= damage;

            if (unit.health <= 0)
            {
                Death(unit.animator);
            }
        }

        private void Death(Animator unitAnimator)
        {
            SetAnim(unitAnimator, ConstsAnimNames.DeathClipName);

            unitCollider.enabled = false;
            unitRigidbody.simulated = false;
            
            Destroy(gameObject, lifeTimeAfterDeath);
        }
    }
}