using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters
{
    public class Player : MonoBehaviour, IMovementInput
    {

        public event Action<float> OnMove;
        public event Action<float> OnSidewayMovement;
        public event Action<float> OnRotation;

        private void Awake()
        {
            //Input.
        }

        private void Update()
        {
            
        }
    }
}
