using IndustrRazvlProj.Characters;
using System;
using UnityEngine;

namespace IndustrRazvlProj
{
    [Serializable]
    public class ScoreData
    {
        [field: SerializeField] public CharacterFactions Faction { get; private set; }
        [field: SerializeField] public int Score { get; set; } = 0;
    }
}
