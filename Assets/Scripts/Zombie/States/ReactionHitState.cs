using FSM;
using UnityEngine;

namespace Zombie.States
{
    public class ReactionHitState : State
    {
        private ZombieAnimations _animations;
        public ReactionHitState(ZombieAnimations animations)
        {
            _animations = animations;
        }

        public override void Enter()
        {
            _animations.PlayReactionHit();
        }
    }
}
