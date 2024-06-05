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
            // If not attacking or target not in radius -> ChaseState
            if (!_enemy.IsAttacking || _enemy.CurrentTargetTransform is null)
                _stateMachine.ChangetState(_enemy.ChaseState);
        }

        public override void BehaviorUpdate()
        {
            // If target in attack radius and in chase radius
            if (_enemy.IsAttacking && _enemy.CurrentTargetTransform is not null)
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                // If can shoot in target
                if (_enemy.ShotAvailability(direction))
                {
                    float angle = Vector2.SignedAngle(_enemy.transform.up, direction);
                    // Shoot the target if it is ahead
                    if (Math.Abs(angle) < _enemy.AttackAccuracy)
                    {
                        _enemy.Fire();
                    }
                    // Otherwise turn to the target
                    else
                    {
                        _enemy.Rotate(angle);
                    }
                }
            }
        }
    }
}
