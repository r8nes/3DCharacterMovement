using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Core.State
{
    public class StateMachine
    {
        protected IState CurrentState;

        public void ChangeState(IState newState) 
        {
            CurrentState?.Exit();
            CurrentState = newState; 
            CurrentState?.Enter();
        }

        public void HandleInput()
        {
            CurrentState?.HandleInput();
        }

        public void Update()
        {
            CurrentState?.Update();
        }
        
        public void PhysicsUpdate()
        {
            CurrentState?.PhysicsUpdate();
        }
    }
}
