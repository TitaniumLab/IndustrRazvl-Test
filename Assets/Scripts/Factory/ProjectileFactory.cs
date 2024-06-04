using IndustrRazvlProj.Pools;
using Unity.VisualScripting;
using UnityEngine;

namespace IndustrRazvlProj.Factories
{
    public class ProjectileFactory
    {
        private Transform _projPrefab;
        private Transform _parentTransform;
        private CustomProjectilesPool _projectilesPool;
        private float _rbGravScale = 0;
        private bool _freezRotation;

        public ProjectileFactory(Transform projPrefab, Transform parentTransform, CustomProjectilesPool projectilesPool)
        {
            _projPrefab = projPrefab;
            _parentTransform = parentTransform;
            _projectilesPool = projectilesPool;
        }

        public Projectile CreateProjectile()
        {
            var obj = Object.Instantiate(_projPrefab, _parentTransform);
            var rb = obj.AddComponent<Rigidbody2D>();
            rb.gravityScale = _rbGravScale;
            rb.freezeRotation = _freezRotation;
            obj.AddComponent<CircleCollider2D>();
            var proj = obj.AddComponent<Projectile>();
            // obj.AddComponent<Rigidbody2D>();
            proj.Init(_projectilesPool);
            return proj;
        }
    }
}
