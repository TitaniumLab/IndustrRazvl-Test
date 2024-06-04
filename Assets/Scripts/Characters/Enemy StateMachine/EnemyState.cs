using UnityEngine;

namespace IndustrRazvlProj.Characters.StateMachines
{
    public class EnemyState
    {
        protected Enemy _enemy;
        protected EnemyStateMachine _stateMachine;

        protected EnemyState(Enemy enemy, EnemyStateMachine machine)
        {
            _enemy = enemy;
            _stateMachine = machine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void LogicUpdate() { }
        public virtual void BehaviorUpdate() { }
    }
}
