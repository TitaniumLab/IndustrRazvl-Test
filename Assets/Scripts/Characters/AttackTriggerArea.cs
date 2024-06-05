using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters.TriggerAreas
{
    public class AttackTriggerArea : MonoBehaviour
    {
        public event Action<Transform> OnTriggetEnter;
        public event Action<Transform> OnTriggetExit;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable) && collision.transform is not null)
            {
                OnTriggetEnter?.Invoke(collision.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                OnTriggetExit?.Invoke(collision.transform);
            }
        }
    }
}
