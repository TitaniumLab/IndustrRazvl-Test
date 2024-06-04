using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndustrRazvlProj.Characters.StateMachines
{
    public class EnemyStateMachine 
    {
        public EnemyState CurrentState { get; private set; }

        public void Init(EnemyState state)
        {
            CurrentState = state;
            CurrentState.Enter();
        }

        public void ChangetState(EnemyState state)
        {
            CurrentState.Exit();
            Init(state);
        }
    }
}
