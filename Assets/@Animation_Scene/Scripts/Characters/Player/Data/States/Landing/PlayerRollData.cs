using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerRollData 
    {
        [field: SerializeField] [field: Range(0f, 3f)] public float SpeedModid { get; private set; } = 1f;
    }
}
