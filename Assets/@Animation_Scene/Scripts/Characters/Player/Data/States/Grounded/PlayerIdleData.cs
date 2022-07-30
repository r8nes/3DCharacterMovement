using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.Data
{
    [Serializable]
    public class PlayerIdleData 
    {
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecentering { get; private set; }
    }
}
