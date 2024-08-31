using ArkheroClone.Services.DI;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public class GameStateMachine: BaseStateMachine
    {
        public GameStateMachine(ICoroutineRunner coroutineRunner, DiContainer projectDiContainer)
        {
            _states = new()
            {
                [typeof(BootstrapState)] = new BootstrapState(coroutineRunner,this, projectDiContainer),
                [typeof(NewSessionState)] = new NewSessionState(this, projectDiContainer),
                [typeof(MainMenuState)] = new MainMenuState(this, projectDiContainer),
                [typeof(LoadLevelState)] = new LoadLevelState(this),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
    }
}