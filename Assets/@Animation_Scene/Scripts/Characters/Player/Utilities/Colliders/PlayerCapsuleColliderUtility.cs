using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using UnityEngine;

namespace ActionCatGame.Core.Utilities
{
    [Serializable]
    public class PlayerCapsuleColliderUtility : CapsuleColliderUtility
    {
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }
    }
}
