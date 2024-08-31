using ArkheroClone.Infrastructure.StateMachine;
using ArkheroClone.Services.DI;
using UnityEngine;

namespace ArkheroClone.Infrastructure
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;
        private DiContainer _projectContainer;
        
        private void Awake()
        {
            _projectContainer = new DiContainer();
            StartGame(_projectContainer);
            DontDestroyOnLoad(gameObject);
        }

        private void StartGame(DiContainer projectContainer)
        {
            _gameStateMachine = new(this ,projectContainer);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}