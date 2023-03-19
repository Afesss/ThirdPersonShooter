using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace Zombie
{
    public class ZombieAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private const string _punch = "Punch";
        private const string _reactionHit = "ReactionHit";
        private readonly int _moveCash = Animator.StringToHash("Move");
        private readonly int _idleCash = Animator.StringToHash("Idle");
        private readonly int _punchCash = Animator.StringToHash("Punch");
        private readonly int _reactionHitCash = Animator.StringToHash("ReactionHit");
        private readonly int _dieCash = Animator.StringToHash("Die");

        public bool IsPunchAnimation => animator.GetCurrentAnimatorStateInfo(0).IsName(_punch);
        public bool IsReactionHitAnimation => animator.GetCurrentAnimatorStateInfo(0).IsName(_reactionHit);
        
        public void PlayMove(bool value)
        {
            animator.SetBool(_moveCash, value);
        }

        public void PlayIdle(bool value)
        {
            animator.SetBool(_idleCash, value);
        }

        public void PlayPunch()
        {
            animator.SetTrigger(_punchCash);
        }

        public void PlayReactionHit()
        {
            animator.SetTrigger(_reactionHitCash);
        }

        public void PlayDie()
        {
            animator.SetTrigger(_dieCash);
        }
    }
}
