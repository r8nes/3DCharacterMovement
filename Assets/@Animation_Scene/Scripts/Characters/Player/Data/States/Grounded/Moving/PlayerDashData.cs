using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField] [field: Range(1f, 3f)] public float SpeedModif { get; private set; } = 2f;
        [field: SerializeField] [field: Range(0f, 2f)] public float TimeToBeConsiderRow { get; private set; } = 1f;
        [field: SerializeField] [field: Range(1f, 10f)] public int DashesLimit { get; private set; } = 2;
        [field: SerializeField] [field: Range(0f, 3f)] public float DashLimitCooldown { get; private set; } = 1.75f;
    }
}
