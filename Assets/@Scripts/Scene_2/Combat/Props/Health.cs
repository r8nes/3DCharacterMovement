using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Stats
{
    public class Health : MonoBehaviour
    {
        private float _health;

        public event Action OnTakeDamage; 
        public event Action OnDie;

        [SerializeField] private float _maxHealth = 100f;
        
        void Start()
        {
            _health = _maxHealth;
        }

        public void DealDamage(int damage) 
        {
            if (_health <= 0) return;

            _health = Mathf.Max(_health - damage, 0);

            OnTakeDamage?.Invoke();

            if (_health == 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}
