using System;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerRotationData
    {
        [field:SerializeField]public Vector3 TargetRotationReachTime { get; private set; }

    }
}
