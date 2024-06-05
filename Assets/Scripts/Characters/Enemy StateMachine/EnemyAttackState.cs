using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters.StateMachines
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(Enemy enemy, EnemyStateMachine machine) : base(enemy, machine) { }


        public override void Enter()
        {
            Debug.Log("Enemy AttckState");
        }
        public override void LogicUpdate()
        {
            if (!_enemy.IsAttacking || _enemy.CurrentTargetTransform is null)
                _stateMachine.ChangetState(_enemy.ChaseState);
        }

        public override void BehaviorUpdate()
        {
            if (_enemy.IsAttacking && _enemy.CurrentTargetTransform is not null)
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                if (_enemy.ShotAvailability(direction))
                {
                    float angle = Vector2.SignedAngle(_enemy.transform.up, direction);
                    if (Math.Abs(angle) < _enemy.AttackAccuracy)
                    {
                        _enemy.Fire();
                    }
                    else
                    {
                        _enemy.Rotate(angle);
                    }
                }
            }
        }
    }
}
