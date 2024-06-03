using System;

namespace IndustrRazvlProj
{
    public interface IMovementInput 
    {
        public event Action<float> OnMove;
        public event Action<float> OnSidewayMovement;
        public event Action<float> OnRotation;
    }
}
