using ActionCatGame.Prototype.Combat;
using ActionCatGame.Prototype.Enemy;
using ActionCatGame.Prototype.Props;
using ActionCatGame.Prototype.State;
using ActionCatGame.Prototype.Stats;
using ActionCatGame.Prototype.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace ActionCatGame
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public WeaponDamage Damage { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Target Target { get; private set; }
        [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
        [field: SerializeField] public int AttackDamage { get; private set; }
        [field: SerializeField] public int Knockback { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }

        public Health Player { get; private set; }

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

            Agent.updatePosition = false;
            Agent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        }

        private void OnEnable()
        {
            Health.OnTakeDamage += HandleTakeDamage;
            Health.OnDie += HandleDie;
        }

        private void OnDisable()
        {
            Health.OnTakeDamage -= HandleTakeDamage;
            Health.OnDie -= HandleDie;
        }

        private void HandleTakeDamage()
        {
            SwitchState(new EnemyImpactState(this));
        }

        private void HandleDie()
        {
            SwitchState(new EnemyDeadState(this));
        }
    }
}
