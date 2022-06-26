using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Stats
{
    public class Health : MonoBehaviour
    {
        private float _health;
        private bool _isInvunerable=false;
        [SerializeField] private float _maxHealth = 100f;

        public event Action OnTakeDamage; 
        public event Action OnDie;

        public bool IsDead => _health == 0;
        
        void Start()
        {
            _health = _maxHealth;
        }

        public void DealDamage(int damage) 
        {
            if (_health <= 0) return;

            if (_isInvunerable) return;
            
            _health = Mathf.Max(_health - damage, 0);

            OnTakeDamage?.Invoke();

            if (_health == 0)
            {
                OnDie?.Invoke();
            }
        }

        public void SetInvulnerable(bool isInvunerable) 
        {
            _isInvunerable = isInvunerable;
        }
    }
}
