using IndustrRazvlProj.Characters;
using UnityEngine;

namespace IndustrRazvlProj
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _minDamageTaken;
        [field: SerializeField] public CharacterFactions ÑharacterFaction { get; private set; }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage >= _minDamageTaken)
            {
                _currentHealth -= damage;
            }
        }
    }
}
