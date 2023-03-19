using System.Collections;
using FSM;
using UnityEngine;

namespace Zombie.States
{
    public class DieState : State
    {
        private readonly ZombieAnimations _animations;
        private readonly ZombieRagdoll _ragdoll;

        public DieState(ZombieAnimations animations, ZombieRagdoll ragdoll)
        {
            _animations = animations;
            _ragdoll = ragdoll;
        }

        public override void Enter()
        {
            _animations.PlayDie();
            _ragdoll.StartCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(1);
            _ragdoll.EnableRagdoll();
            yield return new WaitForSeconds(30);
            Object.Destroy(_ragdoll.gameObject);
        }
    }
}
