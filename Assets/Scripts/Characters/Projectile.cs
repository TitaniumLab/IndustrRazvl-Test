using IndustrRazvlProj.Characters;
using IndustrRazvlProj.Pools;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace IndustrRazvlProj
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _lastDireaction;
        [SerializeField] private int _damage;
        [SerializeField] private CharacterFactions _targetFaction;
        private float _endDuration;
        private CustomProjectilesPool _projectilesPool;
        private Rigidbody2D _rb;

        private void Awake()
        {
            if (!TryGetComponent(out _rb))
                throw new Exception($"Missing {typeof(Rigidbody2D)}");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // If target can take damage
            if (collision.collider.TryGetComponent(out IDamagable damagable))
            {
                // If target faction is correct
                if (damagable.ÑharacterFaction == _targetFaction)
                {
                    damagable.TakeDamage(_damage);
                }

                _projectilesPool.Release(this);
            }

            // Ricochet
            Vector2 direction = Vector2.Reflect(_lastDireaction.normalized, collision.contacts[0].normal);
            transform.up = direction;
            _lastDireaction = direction;
            _rb.velocity = direction * _speed;
        }

        public void Init(CustomProjectilesPool projectilesPool)
        {
            _projectilesPool = projectilesPool;
        }

        public void Fire(int damage, float speed, float duration, CharacterFactions targerFaction)
        {
            _speed = speed;
            _damage = damage;
            _targetFaction = targerFaction;
            _lastDireaction = transform.up;
            _rb.velocity = transform.up * speed;
            LifeTimer(duration);
        }

        private async void LifeTimer(float duration)
        {
            _endDuration = Time.time + duration;
            while (Time.time < _endDuration)
            {
                await Task.Yield();
                if (this.IsDestroyed() || !isActiveAndEnabled)
                {
                    return;
                }
            }
            _projectilesPool.Release(this);
        }
    }
}
