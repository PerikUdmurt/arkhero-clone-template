using ArkheroClone.Infrastructure.Timer;
using ArkheroClone.Services.DI;
using UnityEngine;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public class GameLoopState : IPayloadedState<DiContainer>
    {
        public GameLoopState(BaseStateMachine gameStateMachine)
        {

        }

        public async void Enter(DiContainer payload)
        {
            CustomTimer customTimer = new CustomTimer();
            await customTimer.StartCountdown(3);
            Debug.Log("RoundStarted");
        }

        public void Exit()
        {

        }


    }
}