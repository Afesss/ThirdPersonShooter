using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie.States
{
    public class MoveState : State
    {
        private readonly ZombieAnimations _animations;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _target;

        public MoveState(ZombieAnimations animations, NavMeshAgent navMeshAgent, Transform target)
        {
            _animations = animations;
            _navMeshAgent = navMeshAgent;
            _target = target;
        }

        public override void Enter()
        {
            _animations.PlayMove(true);
            _navMeshAgent.enabled = true;
        }

        public override void Exit()
        {
            _animations.PlayMove(false);
            _navMeshAgent.enabled = false;
        }

        public override void Update()
        {
            base.Update();
            _navMeshAgent.SetDestination(_target.position);
        }
    }
}
