using UnityEngine;

namespace IndustrRazvlProj
{
    [RequireComponent(typeof(Collider2D))]
    public class ArenaBounds : MonoBehaviour
    {
        [SerializeField] private float _knockBackDistance = 1;

        private void OnTriggerExit2D(Collider2D collision)
        {

            if (collision.TryGetComponent(out IMovementInput component))
            {
                Vector2 direction = collision.transform.position - transform.position;
                Vector2 knockBackPos = direction - direction.normalized * _knockBackDistance;
                collision.transform.position = knockBackPos;
            }
        }
    }
}
