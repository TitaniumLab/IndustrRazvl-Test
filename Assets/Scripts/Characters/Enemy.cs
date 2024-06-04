using IndustrRazvlProj.Characters.StateMachines;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndustrRazvlProj.Characters
{
    public class Enemy : MonoBehaviour, IMovementInput, IAttackInput
    {
        // Route parameters
        [SerializeField] private Transform RouteParent; // Parent of route points
        public List<Transform> RoutePoints { get; private set; }
        [field: SerializeField] public int CurrentPointIndex { get; private set; }
        [field: SerializeField] public float IdolTime { get; private set; } // Waiting time at route point
        // Attack parameters 
        [field: SerializeField] public bool IsAttacking { get; private set; } 
        [field: SerializeField] public Transform CurrentTargetTransform { get; private set; } 
        [field: SerializeField] public Vector3 AgroPos { get; private set; } // Position to move when seeing an enemy


        public event Action<float> OnMove;
        public event Action<float> OnSidewayMovement;
        public event Action<float> OnRotation;
        public event Action OnFire;

        public EnemyStateMachine StateMachine { get; private set; }
        public IdolState EnemyIdolState { get; private set; }
        public PatrolState EnemyPatrolState { get; private set; }
        public ChaseState EnemyChaseState { get; private set; }
        public AttackState EnemyAttackState { get; private set; }



        #region MonoBeh
        private void Awake()
        {
            int childrenNum = RouteParent.childCount;
            for (int i = 0; i < childrenNum; i++)
            {
                RoutePoints.Add(RouteParent.GetChild(i).transform);
            }


            StateMachine = new EnemyStateMachine();
            EnemyIdolState = new IdolState(this, EnemyStateMachine);
            EnemyPatrolState = new PatrolState(this, EnemyStateMachine);
            EnemyChaseState = new ChaseState(this, EnemyStateMachine);
            EnemyAttackState = new AttackState(this, EnemyStateMachine);

            StateMachine.Init(EnemyIdolState);



            ChaseTriggerArea chaseTriggerArea = GetComponentInChildren<ChaseTriggerArea>();
            chaseTriggerArea.OnTriggetEnter += TriggetEnemyChase;
            chaseTriggerArea.OnTriggetExit += DisableEnemyChase;

            AttackTriggerArea attackTriggerArea = GetComponentInChildren<AttackTriggerArea>();
            attackTriggerArea.OnTriggetEnter += EnableAttack;
            attackTriggerArea.OnTriggetExit += DisableAttack;

            CurrentTargetTransform = null;
        }


        private void FixedUpdate()
        {
            EnemyStateMachine.CurrentState.LogicUpdate();
            EnemyStateMachine.CurrentState.BehaviorUpdate();
        }
        #endregion


        #region methods
        public void Move()
        {
            OnMove?.Invoke();
        }

        public void Rotate(Vector3 direction)
        {
            OnRotation?.Invoke(direction);
        }

        public void Fire()
        {
            OnFire.Invoke();
        }

        private void TriggetEnemyChase(IDamagable damagable, Transform transform)
        {
            if (damagable.UnitFraction == TargetFraction)
            {
                CurrentTargetTransform = transform;
            }
        }

        private void DisableEnemyChase(Transform targetTransform)
        {
            if (targetTransform = CurrentTargetTransform)
            {
                CurrentTargetTransform = null;
            }
        }

        private void EnableAttack(Transform targetTransform)
        {
            if (CurrentTargetTransform is not null)
                IsAttacking = true;
        }

        private void DisableAttack(Transform targetTransform)
        {
            IsAttacking = false;
        }

        public void Agro(Vector3 agroPos)
        {
            AgroPos = agroPos;
        }

        public void ToNextRoutePoint()
        {
            CurrentPointIndex = (CurrentPointIndex + 1) % RoutePoints.Count;
        }
        #endregion
    }
}
