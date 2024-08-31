using ArkheroClone.Gameplay.Platforms;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Services.DI;
using ArkheroClone.Services.SceneLoader;
using ArkheroClone.Services.Scores;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public class NewSessionState : IPayloadedState<GameMode>
    {
        private readonly BaseStateMachine _gameStateMachine;
        private readonly DiContainer _sessionContainer;

        public NewSessionState(BaseStateMachine gameStateMachine, DiContainer projectContainer)
        {
            _gameStateMachine = gameStateMachine;
            _sessionContainer = new(projectContainer);
        }

        public void Enter(GameMode payload)
        {
            RegisterInfiniteGameMode();
        }

        private void RegisterInfiniteGameMode()
        {
            RegisterScoreSystem();
            RegisterPlatformSpawner();
            LoadLevel();
        }

        private void RegisterScoreSystem(int initialScore = 0)
        {
            ScoreSystem scoreSystem = new ScoreSystem(initialScore);

            _sessionContainer.RegisterInstance(scoreSystem);
        }

        private void LoadLevel()
        {
            LevelPayload levelPayload = new(SceneName.GameplayScene,_sessionContainer);
            _gameStateMachine.Enter<LoadLevelState, LevelPayload>(levelPayload);
        }

        private void RegisterPlatformSpawner()
        {
            _sessionContainer.Register<PlatformSpawner>(
                DiRegistrationType.AsTransient,
                factory: c => new(_sessionContainer.Resolve<IBundleProvider>()));
        }

        public void Exit()
        {
            
        }
    }
}