using IndustrRazvlProj.Characters;
using IndustrRazvlProj.EventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace IndustrRazvlProj
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private ScoreData[] _scoreData;
        private CustomEventBus _eventBus;

        [Inject]
        private void Construct(CustomEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _eventBus.Subscribe<DeathSignal>(OnDeath);
            _eventBus.Subscribe<GameStartSignal>(OnGameStart);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<DeathSignal>(OnDeath);
        }

        private void OnDeath(DeathSignal signal)
        {
            AddScoreTo(signal.DeadCharacter);
            SetScoreText();
        }

        private void OnGameStart(GameStartSignal signal)
        {
            SetScoreText();
        }

        private void AddScoreTo(CharacterFactions faction)
        {
            foreach (var item in _scoreData)
            {
                if (faction != item.Faction)
                {
                    item.Score++;
                }
            }
        }

        private void SetScoreText()
        {
            string scoreText = null;
            foreach (var item in _scoreData)
            {
                string factionName = item.Faction.ToString();
                scoreText += $"{factionName}: {item.Score} ";
            }
            _scoreText.text = scoreText;
        }
    }
}

