using System;
using Test.Scripts;
using UnityEngine;

namespace TaskArcher.Scripts.Units
{
    public class Player : Unit
    {
        public Action<Animator, string> onChangeAnim;
        public Action<Unit, int> onTakeDamage;
        
        private void OnEnable()
        {
            onChangeAnim += SetAnim;
            Weapon.onAttack += AttackRange;
            onTakeDamage += TakeDamage;
        }

        private void OnDisable()
        {
            onChangeAnim -= SetAnim;
            Weapon.onAttack -= AttackRange;
            onTakeDamage -= TakeDamage;
        }

        private void AttackRange()
        {
            SetAnim(Animator, ConstsAnimNames.AttackFinishClipName);
        }
    }
}