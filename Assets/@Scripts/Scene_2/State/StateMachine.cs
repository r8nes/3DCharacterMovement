using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _state;

        private void Update()
        {
            float delta = Time.deltaTime;
            _state?.Tick(delta);
        }

        public void SwitchState(State newState)
        {
            _state?.Exit();
            _state = newState;
            _state?.Enter();
        }
    }
}
