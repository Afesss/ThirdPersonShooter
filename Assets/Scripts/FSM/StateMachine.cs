using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine
    {
        private State _currentState;
        private Dictionary<Type, State> _states;

        public StateMachine(Dictionary<Type, State> states, Type initialState)
        {
            _states = states;
            ChangeState(initialState);
        }

        public void ChangeState<T>(T newState) where T : Type
        {
            if (_states.ContainsKey(newState))
            {
                if(_currentState == _states[newState])
                    return;
                _currentState?.Exit();
                _currentState = _states[newState];
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"State {newState} not set");
            }
                
        }

        public void UpdateState()
        {
            if(_currentState.UpdateTransitions())
                return;
            
            _currentState.Update();
        }
    }
}