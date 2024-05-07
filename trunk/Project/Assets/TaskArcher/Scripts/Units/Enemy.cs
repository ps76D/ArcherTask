using System;
using UnityEngine;

namespace TaskArcher.Scripts.Units
{
    public class Enemy : Unit
    {
        public Action<Animator, string> onChangeAnim;
        public Action<Unit, int> onTakeDamage;
        
        private void OnEnable()
        {
            onChangeAnim += SetAnim;
            onTakeDamage += TakeDamage;
        }

        private void OnDisable()
        {
            onChangeAnim -= SetAnim;
            onTakeDamage -= TakeDamage;
        }
    }
}