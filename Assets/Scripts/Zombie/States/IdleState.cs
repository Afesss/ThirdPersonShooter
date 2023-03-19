using FSM;
using UnityEngine;

namespace Zombie.States
{
    public class IdleState : State
    {
        private readonly ZombieAnimations _animations;

        public IdleState(ZombieAnimations animations)
        {
            _animations = animations;
        }

        public override void Enter()
        {
            _animations.PlayIdle(true);
        }

        public override void Exit()
        {
            _animations.PlayIdle(false);
        }
    }
}
