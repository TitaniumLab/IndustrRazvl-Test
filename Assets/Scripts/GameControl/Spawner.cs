using IndustrRazvlProj.EventBus;
using UnityEngine;
using Zenject;

namespace IndustrRazvlProj.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawnData;
        private CustomEventBus _eventBus;

        [Inject]
        private void Construst(CustomEventBus bus)
        {
            _eventBus = bus;
        }

        private void Awake()
        {
            _eventBus.Subscribe<DeathSignal>(OnDeath);
            _eventBus.Subscribe<GameStartSignal>(OnGameStart);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<DeathSignal>(OnDeath);
            _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
        }

        private void OnDeath(DeathSignal signal)
        {
            NewRound();
        }

        private void OnGameStart(GameStartSignal signal)
        {
            NewRound();
        }

        private void NewRound()
        {
            foreach (var data in _spawnData)
            {
                data.Character.ResetHealth();
                data.Character.transform.position = data.SpawnPos.position;
            }
        }
    }
}
