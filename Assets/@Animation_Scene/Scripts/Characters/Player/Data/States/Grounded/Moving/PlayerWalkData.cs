using System;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField] [field: Range(0, 1f)] public float SpeedModif { get; private set; } = 0.225f;
    }
}
