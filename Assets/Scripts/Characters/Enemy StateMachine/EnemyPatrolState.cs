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
            if (distance < _enemy.DistanceToStop)
                _stateMachine.ChangetState(_enemy.IdolState);

            if (_enemy.CurrentTargetTransform is not null)
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                if (_enemy.CheckPassability(direction))
                {
                    _stateMachine.ChangetState(_enemy.ChaseState);
                }
            }
        }

        public override void BehaviorUpdate()
        {
            Vector2 direction = _enemy.RoutePoints[_enemy.CurrentPointIndex].transform.position - _enemy.transform.position;
            if (_enemy.CheckPassability(direction))
            {
                float angle = Vector2.SignedAngle(_enemy.transform.up, direction);
                if (Math.Abs(angle) < _enemy.MovementAccuracy)
                {
                    _enemy.Move();
                }
                else
                {
                    _enemy.Rotate(angle);
                }
            }
            else
            {
                _enemy.ToNextRoutePoint();
            }
        }
    }
}
