using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters.Movement
{
    [RequireComponent(typeof(IMovementInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class DefalutMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _sideSpeed;
        private IMovementInput _movable;
        private Rigidbody2D _rb;

        private void Awake()
        {
            if (!TryGetComponent(out _movable))
                throw new Exception($"Missing {typeof(IMovementInput).Name} component.");
            if (!TryGetComponent(out _rb))
                throw new Exception($"Missing {typeof(Rigidbody2D).Name} component.");

            _movable.OnMove += Move;
            _movable.OnRotation += Rotate;
            _movable.OnSidewayMovement += SidewayMovement;
        }

        private void OnDestroy()
        {
            _movable.OnMove -= Move;
            _movable.OnRotation -= Rotate;
            _movable.OnSidewayMovement -= SidewayMovement;
        }

        /// <summary>
        /// Move object in direction.
        /// </summary>
        /// <param name="direction">Positive values ​​= forward. Negative values ​​= back.</param>
        private void Move(float direction)
        {
            _rb.velocity = transform.TransformDirection(Vector3.up) * direction * _movementSpeed;
        }

        /// <summary>
        /// Rotate object in direction.
        /// </summary>
        /// <param name="direction">Positive values ​​= right. Negative values ​​= left.</param>
        private void Rotate(float direction)
        {
            float normDirection = direction / Math.Abs(direction);
            float angle = normDirection * _rotationSpeed * Time.deltaTime;
            Vector3 rotateDirection = Quaternion.Euler(new Vector3(0, 0, angle)).eulerAngles;
            transform.Rotate(rotateDirection);
        }

        /// <summary>
        /// Move object sideways.
        /// </summary>
        /// <param name="direction">Positive values ​​= right. Negative values ​​= left.</param>
        private void SidewayMovement(float direction)
        {
            _rb.velocity = transform.TransformDirection(Vector3.right) * direction * _sideSpeed;
        }
    }
}
