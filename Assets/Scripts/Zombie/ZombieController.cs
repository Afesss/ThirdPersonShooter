using System;
using System.Collections.Generic;
using Architecture.Services;
using FSM;
using UnityEngine;
using UnityEngine.AI;
using Zombie.States;

namespace Zombie
{
    [RequireComponent(typeof(NavMeshAgent), typeof(ZombieAnimations))]
    public class ZombieController : MonoBehaviour
    {
        [SerializeField] private ZombieRagdoll ragdoll;
        [SerializeField] private Collider zombieCollider;
        [SerializeField, Range(0, 2)] private float distanceToPunch = 1;

        private bool _isHit;
        private bool _isDie;

        private Transform _target;
        private StateMachine _stateMachine;
        private IdleState _idleStateState;
        private MoveState _moveStateState;
        private PunchState _punchState;
        private ReactionHitState _reactionHitState;
        private DieState _dieState;
        private ZombieAnimations _animations;
        private NavMeshAgent _navMeshAgent;
        private GameData _gameData;

        private void Update()
        {
            if (_gameData.HeroDie)
            {   
                _stateMachine.ChangeState(typeof(IdleState));
                return;
            }
            _stateMachine.UpdateState();
        }

        public void Initialize(Transform heroTransform, GameData gameData)
        {
            _target = heroTransform;
            _gameData = gameData;
            
            _animations = GetComponent<ZombieAnimations>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            CreateStates();
            CreateStateMachine();
        }

        public void InvokeHit()
        {
            _isHit = true;
        }

        public void InvokeDie()
        {
            _isDie = true;
            zombieCollider.enabled = false;
            _gameData.IncreaseDieZombieValue();
        }

        private void CreateStates()
        {
            _idleStateState = new IdleState(_animations);
            _idleStateState.AddTransition(TransitionToMove);
            _idleStateState.AddTransition(TransitionToPunch);
            _idleStateState.AddTransition(TransitionToReactionHit);
            _idleStateState.AddTransition(TransitionToDie);
            _moveStateState = new MoveState(_animations, _navMeshAgent, _target);
            _moveStateState.AddTransition(TransitionToPunch);
            _moveStateState.AddTransition(TransitionToReactionHit);
            _moveStateState.AddTransition(TransitionToDie);
            _punchState = new PunchState(_animations, _target);
            _punchState.AddTransition(TransitionPunchToIdle);
            _punchState.AddTransition(TransitionToReactionHit);
            _punchState.AddTransition(TransitionToDie);
            _reactionHitState = new ReactionHitState(_animations);
            _reactionHitState.AddTransition(TransitionReactionHitToIdle);
            _reactionHitState.AddTransition(TransitionToDie);
            _dieState = new DieState(_animations, ragdoll);
        }

        private void CreateStateMachine()
        {
            Dictionary<Type, State> states = new Dictionary<Type, State>()
            {
                {_idleStateState.GetType(), _idleStateState},
                {_moveStateState.GetType(), _moveStateState},
                {_punchState.GetType(), _punchState},
                {_reactionHitState.GetType(), _reactionHitState},
                {_dieState.GetType(), _dieState}
            };
            
            _stateMachine = new StateMachine(states, typeof(IdleState));
        }

        private bool TransitionToMove()
        {
            if (_target != null && (transform.position - _target.position).sqrMagnitude > distanceToPunch * distanceToPunch)
            {
                _stateMachine.ChangeState(typeof(MoveState));
                return true;
            }

            return false;
        }

        private bool TransitionToPunch()
        {
            if ((transform.position - _target.position).sqrMagnitude <= distanceToPunch * distanceToPunch)
            {
                _stateMachine.ChangeState(typeof(PunchState));
                return true;
            }

            return false;
        }

        private bool TransitionPunchToIdle()
        {
            if (!_animations.IsPunchAnimation)
            {
                _stateMachine.ChangeState(typeof(IdleState));
                return true;
            }

            return false;
        }

        private bool TransitionToReactionHit()
        {
            if (_isHit)
            {
                _stateMachine.ChangeState(typeof(ReactionHitState));
                _isHit = false;
                return true;
            }

            return false;
        }

        private bool TransitionReactionHitToIdle()
        {
            if (!_animations.IsReactionHitAnimation)
            {
                _stateMachine.ChangeState(typeof(IdleState));
                return true;
            }

            return false;
        }

        private bool TransitionToDie()
        {
            if (_isDie)
            {
                _stateMachine.ChangeState(typeof(DieState));
                return true;
            }

            return false;
        }
    }
}
