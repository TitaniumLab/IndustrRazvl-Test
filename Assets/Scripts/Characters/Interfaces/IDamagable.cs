namespace IndustrRazvlProj.Characters
{
    public interface IDamagable
    {
        public CharacterFactions �haracterFaction { get; }
        public void TakeDamage(int damage);
    }
}
