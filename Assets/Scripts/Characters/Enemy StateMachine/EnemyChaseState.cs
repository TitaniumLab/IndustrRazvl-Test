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
            if (_enemy.CurrentTargetTransform is null)
            {
                _stateMachine.ChangetState(_enemy.IdolState);
            }
            else
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                if (!_enemy.CheckPassability(direction))
                {
                    _stateMachine.ChangetState(_enemy.IdolState);
                }
            }
            if (_enemy.IsAttacking)
                _stateMachine.ChangetState(_enemy.AttackState);
        }

        public override void BehaviorUpdate()
        {
            Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
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

