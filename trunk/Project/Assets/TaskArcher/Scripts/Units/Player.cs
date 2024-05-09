using System;
using UnityEngine;

namespace TaskArcher.Units
{
    public class Player : Unit
    {
        public Action<Animator, string> OnChangeAnim;
        public Action<Unit, float> OnTakeDamage;
        
        private void OnEnable()
        {
            OnChangeAnim += SetAnim;
            Weapon.OnAttack += AttackRange;
            OnTakeDamage += TakeDamage;
        }

        private void OnDisable()
        {
            OnChangeAnim -= SetAnim;
            Weapon.OnAttack -= AttackRange;
            OnTakeDamage -= TakeDamage;
        }

        private void AttackRange()
        {
            SetAnim(Animator, ConstsAnimNames.AttackFinishClipName);
        }
    }
}