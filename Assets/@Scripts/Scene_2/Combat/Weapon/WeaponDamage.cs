using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Props;
using ActionCatGame.Prototype.Stats;
using UnityEngine;

namespace ActionCatGame.Prototype.Weapon
{
    public class WeaponDamage : MonoBehaviour
    {
        private int _damage;
        private float _knockback;
        private bool _blow;

        [SerializeField] private Collider _collider;

        private List<Collider> _collided = new List<Collider>();

        private void OnEnable()
        {
            _collided.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == _collider) return;

            if (_collided.Contains(other)) return;

            _collided.Add(other);

            if (other.TryGetComponent(out Health health))
            {
                health.DealDamage(_damage);
            }

            if (other.TryGetComponent(out ForceReceiver force))
            {
                Vector3 dir = (other.transform.position - _collider.transform.position).normalized; 
                force.AddForce(dir*_knockback, _blow);
            }
        }

        public void SetAttack(int damage, float knockback, bool blow = false) 
        {
            _damage = damage;
            _knockback = knockback;
            _blow = blow;
        }
    }
}