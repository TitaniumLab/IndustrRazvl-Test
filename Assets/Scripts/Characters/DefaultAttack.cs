using IndustrRazvlProj.Pools;
using System;
using UnityEngine;

namespace IndustrRazvlProj.Characters.Attack
{
    [RequireComponent(typeof(IAttackInput))]
    public class DefaultAttack : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private Transform _parentTransform;
        [SerializeField] private Transform _projPrefab;
       [SerializeField] private float _attackSpeed;
        [SerializeField] private float _projSpeed;
        [SerializeField] private float _projDuration;
        [SerializeField] private int _projDamage;
        [SerializeField] private CharacterFactions _targetFaction;
        private float _nextShotTime;
        private IAttackInput _input;
        private CustomProjectilesPool _projectilesPool;

        private void Awake()
        {
            if (!TryGetComponent(out _input))
                throw new Exception($"Missing {typeof(IAttackInput).Name} component.");

            _projectilesPool = new CustomProjectilesPool(_projPrefab, _parentTransform);

            _input.OnFire += Fire;
        }

        private void OnDestroy()
        {
            _input.OnFire -= Fire;
        }

        private void Fire()
        {
            if (Time.time >= _nextShotTime)
            {
                _nextShotTime = Time.time + 1 / _attackSpeed;
                var proj = _projectilesPool.Get();
                proj.transform.position = _shootingPoint.position;
                proj.transform.rotation = transform.rotation;
                proj.Fire(_projDamage, _projSpeed, _projDuration, _targetFaction);
            }
        }
    }
}
