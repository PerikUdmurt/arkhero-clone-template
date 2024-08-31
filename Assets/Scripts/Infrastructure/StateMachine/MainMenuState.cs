using ArkheroClone.Services.DI;
using ArkheroClone.Services.SceneLoader;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _projectDiContainer;

        public MainMenuState(GameStateMachine gameStateMachine, DiContainer projectDiContainer)
        {
            _gameStateMachine = gameStateMachine;
            _projectDiContainer = projectDiContainer;
        }

        public void Enter()
        {
            LoadMenuScene();
        }

        private void LoadMenuScene()
        {
            _projectDiContainer.Resolve<SceneLoader>().Load(SceneName.MainMenuScene, OnLoaded);
            LoadNewSession(GameMode.InfiniteGame);
        }

        private void OnLoaded()
        {
            //Показать кнопки выбора
        }

        public void Exit()
        {
            
        }

        private void LoadNewSession(GameMode gameMode)
        {
            _gameStateMachine.Enter<NewSessionState, GameMode>(gameMode);
        }
    }
}