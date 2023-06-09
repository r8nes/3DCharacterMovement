using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Combat
{
    [Serializable]
    public class Attack
    {
        [field: SerializeField] public string AnimationName { get; private set; }
        [field: SerializeField] public float TransitionDuration { get; private set; }
        [field: SerializeField] public float ComboAttackTime { get; private set; }
        [field: SerializeField] public int CombatStateIndex { get; private set; } = -1;
        [field: SerializeField] public float ForceTime { get; private set; } = -1;
        [field: SerializeField] public float Force { get; private set; } = -1;
        [field: SerializeField] public float KnockBack { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public bool Blow { get; private set; }
    }
}
