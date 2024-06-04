namespace IndustrRazvlProj.Characters
{
    public interface IDamagable
    {
        public CharacterFactions ÑharacterFaction { get; }
        public void TakeDamage(int damage);
    }
}
