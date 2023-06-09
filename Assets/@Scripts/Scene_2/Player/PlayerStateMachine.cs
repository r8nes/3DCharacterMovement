using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Combat;
using ActionCatGame.Prototype.Props;
using ActionCatGame.Prototype.State;
using ActionCatGame.Prototype.Stats;
using ActionCatGame.Prototype.Weapon;
using UnityEngine;

namespace ActionCatGame.Prototype.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public InputReader Input { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public WeaponDamage Damage { get; private set; }
        [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Attack[] Attacks { get; private set; }

        public Transform MainCameraTranform { get; private set; }

        private void Start()
        {
            MainCameraTranform = Camera.main.transform;

            SwitchState(new PlayerFreeLookState(this));
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
            SwitchState(new PlayerImpactState(this));
        }
        private void HandleDie()
        {
            SwitchState(new PlayerDeadState(this));
        }
    }
}
