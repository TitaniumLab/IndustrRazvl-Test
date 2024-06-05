using IndustrRazvlProj.Characters;
using System;
using UnityEngine;

namespace IndustrRazvlProj.Game
{
    [Serializable]
    public class SpawnData
    {
        [field: SerializeField] public Health Character { get; private set; }
        [field: SerializeField] public Transform SpawnPos { get; private set; }
    }
}
