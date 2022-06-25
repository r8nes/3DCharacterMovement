using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Combat
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}
