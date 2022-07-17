using System;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField] [field: Range(0, 25f)] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField] public AnimationCurve SlopeSpeedAngle { get; private set; } 
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
    }
}
