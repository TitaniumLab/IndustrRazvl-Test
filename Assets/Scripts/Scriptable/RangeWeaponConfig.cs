using UnityEngine;

namespace IndustrRazvlProj.Weapons
{
    [CreateAssetMenu(fileName = "New RangeWeaponConfig", menuName = "RangeWeaponConfig", order = 51)]
    public class RangeWeaponConfig : ScriptableObject
    {
        [SerializeField] private Transform _projPrefab;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _projSpeed;
        [SerializeField] private float _projDuration;
        [SerializeField] private int _projDamage;


        public Transform ProjPrefab => _projPrefab;
        public float AttackSpeed => _attackSpeed;
        public float ProjSpeed => _projSpeed;
        public float ProjDuration => _projDuration;
        public int ProjDamage => _projDamage;
    }
}
