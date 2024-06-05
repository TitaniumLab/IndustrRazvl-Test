using IndustrRazvlProj.Characters.StateMachines;
using IndustrRazvlProj.Characters.TriggerAreas;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndustrRazvlProj.Characters
{
    public class Enemy : MonoBehaviour, IMovementInput, IAttackInput
    {
        // Route parameters
        [field: Header("Route parameters")]
        [SerializeField] private Transform _routeParent; // Parent of route points
        public List<Transform> RoutePoints { get; private set; } = new List<Transform>(); // All route points
        [SerializeField] private ContactFilter2D _obstacleFilter;
        [field: SerializeField] public float MovementAccuracy { get; private set; } // Destination visibility angle
        private float _moveInputValue = 1; // Value to send in OnMove event
        [SerializeField] private float _obstacleAvoidRadius = 0.5f; // Radius of sphere on SphereCast. Should use collider radius
        [field: SerializeField] public int CurrentPointIndex { get; private set; }
        [field: SerializeField] public float DistanceToStop { get; private set; } = 0.1f; // Distance from route point to stop
        [field: SerializeField] public float IdolTime { get; private set; } // Waiting time at route point
        // Attack parameters 
        [field: Header("Attack parameters")]
        [SerializeField] private CharacterFactions _targetFaction;
        [field: SerializeField] public float AttackAccuracy { get; private set; } = 2; // Target visibility angle
        [field: SerializeField] public bool IsAttacking { get; private set; }
        [field: SerializeField] public Transform CurrentTargetTransform { get; private set; }
        // Trigger areas
        [field: Header("Trigger areas")]
        [SerializeField] private ChaseTriggerArea _chaseTriggerArea;
        [SerializeField] private AttackTriggerArea _attackTriggerArea;
        // State machine
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyIdolState IdolState { get; private set; }
        public EnemyPatrolState PatrolState { get; private set; }
        public EnemyChaseState ChaseState { get; private set; }
        public EnemyAttackState AttackState { get; private set; }
        // Interfaces events
        public event Action<float> OnMove;
        public event Action<float> OnSidewayMovement;
        public event Action<float> OnRotation;
        public event Action OnFire;

        #region MonoBeh
        private void Awake()
        {
            // Get all route points
            int childrenNum = _routeParent.childCount;
            for (int i = 0; i < childrenNum; i++)
            {
                RoutePoints.Add(_routeParent.GetChild(i).transform);
            }


            // Create state machine
            StateMachine = new EnemyStateMachine();
            IdolState = new EnemyIdolState(this, StateMachine);
            PatrolState = new EnemyPatrolState(this, StateMachine);
            ChaseState = new EnemyChaseState(this, StateMachine);
            AttackState = new EnemyAttackState(this, StateMachine);
            StateMachine.Init(IdolState); // Set initial state


            _chaseTriggerArea.OnTriggetEnter += SetTarget;
            _chaseTriggerArea.OnTriggetExit += RemoveTarget;

            _attackTriggerArea.OnTriggetEnter += EnableAttack;
            _attackTriggerArea.OnTriggetExit += DisableAttack;

            CurrentTargetTransform = null;
        }


        private void Update() // Update method is used due to delta time
        {
            StateMachine.CurrentState.LogicUpdate();
            StateMachine.CurrentState.BehaviorUpdate();
        }

        private void OnDestroy()
        {
            _chaseTriggerArea.OnTriggetEnter -= SetTarget;
            _chaseTriggerArea.OnTriggetExit -= RemoveTarget;

            _attackTriggerArea.OnTriggetEnter -= EnableAttack;
            _attackTriggerArea.OnTriggetExit -= DisableAttack;
        }
        #endregion


        #region methods
        public void Move()
        {
            OnMove?.Invoke(_moveInputValue);
        }

        public void Rotate(float direction)
        {
            OnRotation?.Invoke(direction);
        }

        public void Fire()
        {
            OnFire?.Invoke();
        }

        public void SetTarget(IDamagable damagable, Transform targetTransform)
        {
            // If the target belongs to the faction
            if (damagable.ÑharacterFaction == _targetFaction)
            {
                CurrentTargetTransform = targetTransform;
            }
        }

        public void RemoveTarget(Transform targetTransform)
        {
            if (CurrentTargetTransform == targetTransform)
            {
                CurrentTargetTransform = null;
            }
        }

        public bool CheckPassability(Vector2 direction)
        {
            float distance = direction.magnitude;
            RaycastHit2D[] results = new RaycastHit2D[1];
            if (Physics2D.CircleCast(transform.position, _obstacleAvoidRadius, direction, _obstacleFilter, results, distance) > 0)
                return false;
            return true;
        }

        public bool ShotAvailability(Vector2 direction)
        {
            float distance = direction.magnitude;
            RaycastHit2D[] results = new RaycastHit2D[1];
            if (Physics2D.Raycast(transform.position, direction, _obstacleFilter, results, distance) > 0)
                return false;
            return true;
        }

        private void EnableAttack(Transform targetTransform)
        {
            if (CurrentTargetTransform == targetTransform)
                IsAttacking = true;
        }

        private void DisableAttack(Transform targetTransform)
        {
            if (CurrentTargetTransform == targetTransform)
                IsAttacking = false;
        }

        public void ToNextRoutePoint()
        {
            CurrentPointIndex = (CurrentPointIndex + 1) % RoutePoints.Count;
        }
        #endregion
    }
}
