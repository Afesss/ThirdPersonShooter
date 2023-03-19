using FSM;
using UnityEngine;

namespace Zombie.States
{
    public class PunchState : State
    {
        private readonly ZombieAnimations _animations;
        private readonly Transform _target;

        public PunchState(ZombieAnimations animations, Transform target)
        {
            _animations = animations;
            _target = target;
        }

        public override void Enter()
        {
            _animations.PlayPunch();
            _animations.transform.LookAt(_target);
        }
    }
}
