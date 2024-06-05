using IndustrRazvlProj.Characters;

namespace IndustrRazvlProj.EventBus
{
    public class DeathSignal
    {
        public readonly CharacterFactions DeadCharacter;

        public DeathSignal(CharacterFactions deadCharacter)
        {
            DeadCharacter = deadCharacter;
        }
    }
}
