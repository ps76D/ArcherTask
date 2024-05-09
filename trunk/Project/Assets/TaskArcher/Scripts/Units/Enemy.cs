using System;
using UnityEngine;

namespace TaskArcher.Units
{
    public class Enemy : Unit
    {
        public Action<Animator, string> OnChangeAnim;
        public Action<Unit, float> OnTakeDamage;
        
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