using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters.StateMachines
{
    public class EnemyChaseState : EnemyState
    {
        public EnemyChaseState(Enemy enemy, EnemyStateMachine machine) : base(enemy, machine) { }

        public override void Enter()
        {
            Debug.Log("Enemy ChaseState");
        }

        public override void LogicUpdate()
        {
            // If you lost target -> IdolState
            if (_enemy.CurrentTargetTransform is null)
            {
                _stateMachine.ChangetState(_enemy.IdolState);
            }
            else
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                // State of idol if the target can't be approached
                if (!_enemy.CheckPassability(direction))
                {
                    _stateMachine.ChangetState(_enemy.IdolState);
                }
            }
            // State of Attack if target in attack radius
            if (_enemy.IsAttacking && _enemy.CurrentTargetTransform is not null)
                _stateMachine.ChangetState(_enemy.AttackState);
        }

        public override void BehaviorUpdate()
        {
            // If target in radius
            if (_enemy.CurrentTargetTransform is not null)
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                // If can move to target
                if (_enemy.CheckPassability(direction))
                {
                    float angle = Vector2.SignedAngle(_enemy.transform.up, direction);
                    // Approach the target if it is ahead
                    if (Math.Abs(angle) < _enemy.MovementAccuracy)
                    {
                        _enemy.Move();
                    }
                    // Otherwise turn to the target
                    else
                    {
                        _enemy.Rotate(angle);
                    }
                }
                // If can't move to target -> go to next point
                else
                {
                    _enemy.ToNextRoutePoint();
                }
            }
        }
    }
}

