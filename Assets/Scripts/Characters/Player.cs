using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters
{
    public class Player : MonoBehaviour, IMovementInput, IAttackInput
    {
        private DefaultInputs _inputActions;
        public event Action<float> OnMove;
        public event Action<float> OnSidewayMovement;
        public event Action<float> OnRotation;
        public event Action OnFire;

        private void Awake()
        {
            _inputActions = new DefaultInputs();
        }

        private void OnEnable()
        {
            _inputActions.Gameplay.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Gameplay.Disable();
        }

        private void Update() // Новая inputSystem удобная для биндинга, но патерн наблюдателя курит где-то в сторонке
        {
            // Move forward action
            if (_inputActions.Gameplay.Move.IsPressed())
            {
                float yValue = _inputActions.Gameplay.Move.ReadValue<float>();
                OnMove?.Invoke(yValue);
            }

            // Move sideways action
            if (_inputActions.Gameplay.SidewayMove.IsPressed())
            {
                float xValue = _inputActions.Gameplay.SidewayMove.ReadValue<float>();
                OnSidewayMovement?.Invoke(xValue);
            }

            // Rotate action
            if (_inputActions.Gameplay.Rotate.IsPressed())
            {
                float rotation = _inputActions.Gameplay.Rotate.ReadValue<float>();
                OnRotation?.Invoke(rotation);
            }

            // Fire action
            if (_inputActions.Gameplay.Fire.IsPressed())
            {
                OnFire?.Invoke();
            }
        }
    }
}
