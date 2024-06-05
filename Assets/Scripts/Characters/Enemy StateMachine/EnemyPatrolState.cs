using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters.StateMachines
{
    public class EnemyPatrolState : EnemyState
    {
        public EnemyPatrolState(Enemy enemy, EnemyStateMachine machine) : base(enemy, machine) { }

        public override void Enter()
        {
            Debug.Log("Enemy PatrolState");
        }

        public override void LogicUpdate()
        {
            float distance = (_enemy.RoutePoints[_enemy.CurrentPointIndex].position - _enemy.transform.position).magnitude;
            // State of the idol if arrived at the point
            if (distance < _enemy.DistanceToStop)
                _stateMachine.ChangetState(_enemy.IdolState);

            // Try chase if target in radius
            if (_enemy.CurrentTargetTransform is not null)
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                // State of chase if the target can be approached
                if (_enemy.CheckPassability(direction))
                {
                    _stateMachine.ChangetState(_enemy.ChaseState);
                }
            }
        }

        public override void BehaviorUpdate()
        {
            Vector2 direction = _enemy.RoutePoints[_enemy.CurrentPointIndex].transform.position - _enemy.transform.position;
            // If you can get to the point
            if (_enemy.CheckPassability(direction))
            {
                float angle = Vector2.SignedAngle(_enemy.transform.up, direction);
                // Approach the point if it is ahead
                if (Math.Abs(angle) < _enemy.MovementAccuracy)
                {
                    _enemy.Move();
                }
                // Otherwise turn to the point
                else
                {
                    _enemy.Rotate(angle);
                }
            }
            // If you can't get to the point -> choose next point
            else
            {
                _enemy.ToNextRoutePoint();
            }
        }
    }
}
