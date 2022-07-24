using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerTriggerColliderData
    {
        [field: SerializeField] public BoxCollider GroundCheckCollider { get; private set; }
    }
}
