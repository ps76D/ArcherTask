using System;
using TaskArcher.Units;
using UnityEngine;

namespace TaskArcher.Scripts.Units
{
    public class Enemy : Unit
    {
        public Action<Animator, string> OnChangeAnim;
        public Action<Unit, int> OnTakeDamage;
        
        private void OnEnable()
        {
            OnChangeAnim += SetAnim;
            OnTakeDamage += TakeDamage;
        }

        private void OnDisable()
        {
            OnChangeAnim -= SetAnim;
            OnTakeDamage -= TakeDamage;
        }
    }
}