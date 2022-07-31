using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerJumpData
    {
        [field: SerializeField] [field: Range(0f, 5f)] public float JumpToGroundRayDistance { get; private set; } = 2f;
        [field: SerializeField] [field: Range(0f, 10f)] public float DecelerationForce { get; private set; } = 1.5f;
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
        [field: SerializeField] public AnimationCurve JumpForceModifOnSlopeUp { get; private set; }
        [field: SerializeField] public AnimationCurve JumpForceModifOnSlopeDown { get; private set; }
        [field: SerializeField] public Vector3 StationaryForce { get; private set; }
        [field: SerializeField] public Vector3 WeakForce { get; private set; }
        [field: SerializeField] public Vector3 MediumForce { get; private set; }
        [field: SerializeField] public Vector3 StrongForce { get; private set; }
    }
}
