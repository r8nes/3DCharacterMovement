using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Stats;
using UnityEngine;

namespace ActionCatGame
{
    public class WaeponDamage : MonoBehaviour
    {
        private int _damage;

        [SerializeField] private Collider _playerCollider;

        private List<Collider> _collided = new List<Collider>();

        private void OnEnable()
        {
            _collided.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == _playerCollider) return;

            if (_collided.Contains(other)) return;

            _collided.Add(other);

            if (other.TryGetComponent(out Health health))
            {
                health.DealDamage(_damage);
            }
        }

        public void SetAttack(int damage) 
        {
            _damage = damage;
        }
    }
}
