using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters.StateMachines
{
    public class EnemyIdolState : EnemyState
    {
        private float _idolEndTime;

        public EnemyIdolState(Enemy enemy, EnemyStateMachine machine) : base(enemy, machine) { }

        public override void Enter()
        {
            Debug.Log("Enemy IdolState");
            _idolEndTime = Time.time + _enemy.IdolTime;
        }


        public override void LogicUpdate()
        {
            if (Time.time > _idolEndTime)
            {
                _enemy.ToNextRoutePoint();
                _stateMachine.ChangetState(_enemy.PatrolState);
            }
            if (_enemy.CurrentTargetTransform is not null)
            {
                Vector2 direction = _enemy.CurrentTargetTransform.position - _enemy.transform.position;
                if (_enemy.CheckPassability(direction))
                {
                    _stateMachine.ChangetState(_enemy.ChaseState);
                }
            }
        }
    }
}
