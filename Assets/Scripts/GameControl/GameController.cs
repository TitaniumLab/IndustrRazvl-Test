using IndustrRazvlProj.EventBus;
using UnityEngine;
using Zenject;

namespace IndustrRazvlProj
{
    public class GameController : MonoBehaviour
    {
        private CustomEventBus _eventBus;

        [Inject]
        private void Construct(CustomEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _eventBus.Invoke(new GameStartSignal());
        }
    }
}
