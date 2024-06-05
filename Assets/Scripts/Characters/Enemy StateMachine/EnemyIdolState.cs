using UnityEngine;

namespace IndustrRazvlProj.Characters.StateMachines
{
    public class EnemyIdolState : EnemyState
    {
        private float _idolEndTime; // Waiting time ends

        public EnemyIdolState(Enemy enemy, EnemyStateMachine machine) : base(enemy, machine) { }

        public override void Enter()
        {
            Debug.Log("Enemy IdolState");
            _idolEndTime = Time.time + _enemy.IdolTime;
        }


        public override void LogicUpdate()
        {
            // If the waiting time is over -> patrol
            if (Time.time > _idolEndTime)
            {
                _enemy.ToNextRoutePoint();
                _stateMachine.ChangetState(_enemy.PatrolState);
            }
            // If target in radius -> try chase
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
    }
}
