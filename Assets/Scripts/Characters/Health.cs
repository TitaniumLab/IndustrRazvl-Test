using IndustrRazvlProj.EventBus;
using UnityEngine;
using Zenject;

namespace IndustrRazvlProj.Characters
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _minHealth;
        [SerializeField] private int _minDamageTaken;
        [field: SerializeField] public CharacterFactions ÑharacterFaction { get; private set; }
        private CustomEventBus _eventBus;

        [Inject]
        private void Construct(CustomEventBus customEventBus)
        {
            _eventBus = customEventBus;
        }

        private void Awake()
        {
            ResetHealth();
        }

        public void TakeDamage(int damage)
        {
            if (damage >= _minDamageTaken)
            {
                _currentHealth -= damage;
                // If died -> call event
                if (_currentHealth <= _minHealth)
                {
                    _eventBus.Invoke(new DeathSignal(ÑharacterFaction));
                }
            }
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
    }
}
