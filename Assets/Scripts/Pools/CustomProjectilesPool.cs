using IndustrRazvlProj.Factories;
using UnityEngine;
using UnityEngine.Pool;

namespace IndustrRazvlProj.Pools
{
    public class CustomProjectilesPool
    {
        private ObjectPool<Projectile> _pool;
        private ProjectileFactory _factory;

        public CustomProjectilesPool(Transform projPrefab, Transform parentTransform)
        {
            _pool = new ObjectPool<Projectile>(CreateProjectile, GetUnit, ReleaseUnit);
            _factory = new ProjectileFactory(projPrefab, parentTransform, this);
        }

        public Projectile Get()
        {
            return _pool.Get();
        }

        public void Release(Projectile proj)
        {
            if (proj.enabled)
            {
                _pool.Release(proj);
            }
        }

        private Projectile CreateProjectile()
        {
            var proj = _factory.CreateProjectile();
            return proj;
        }

        private void GetUnit(Projectile proj) =>
                proj.gameObject.SetActive(true);


        private void ReleaseUnit(Projectile proj) =>
            proj.gameObject.SetActive(false);
    }
}

