using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public class PlayerStateMachine : StateMachine
    {
       [field: SerializeField] public InputReader Input { get; private set; }

        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
    }
}
