using ArkheroClone.Services.DI;
using ArkheroClone.Services.SceneLoader;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public class LevelPayload
    {
        public DiContainer SessionDiContainer;
        public SceneName SceneName;

        public LevelPayload(SceneName sceneName, DiContainer sessionDiContainer)
        {
            SessionDiContainer = sessionDiContainer;
            SceneName = sceneName;
        }
    }
}