using ArkheroClone.Datas;
using ArkheroClone.Gameplay.Characters;
using ArkheroClone.Infrastructure.Timer;
using ArkheroClone.Services.DI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public sealed class GameLoopState : IPayloadedState<DiContainer>
    {
        private readonly BaseStateMachine _gameStateMachine;
        private DiContainer _sceneContainer;

        public GameLoopState(BaseStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter(DiContainer payload)
        {
            _sceneContainer = payload;
            await StartTimer(payload);
            SetAllCharacterActive();
        }

        public void Exit()
        {

        }

        private async UniTask StartTimer(DiContainer payload)
        {
            CustomTimer customTimer = payload.Resolve<CustomTimer>();
            await customTimer.StartCountdown(3);
            Debug.Log("RoundStarted");
        }

        private void SetAllCharacterActive()
        {
            CurrencyLevelData currencyLevelData = _sceneContainer.Resolve<CurrencyLevelData>();
            foreach (Character character in currencyLevelData.Characters)
            {
                character.enabled = true;
            }
        }
    }
}