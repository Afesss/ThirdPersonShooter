using System;
using System.Collections.Generic;

namespace FSM
{
    public abstract class State
    {
        private List<Func<bool>> _transitions = new List<Func<bool>>();
        public virtual void Enter(){}
        
        public virtual  void Exit(){}

        public virtual void Update(){}
        
        public void AddTransition(Func<bool> transition)
        {
            _transitions.Add(transition);
        }

        public bool UpdateTransitions()
        {
            foreach (var transition in _transitions)
            {
                bool? response = transition?.Invoke();

                if (response is true)
                    return true;
            }

            return false;
        }
    }
}
