using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Infrastructure.Factory;
using ArkheroClone.Services;
using ArkheroClone.Services.DI;
using ArkheroClone.Services.SceneLoader;
using ArkheroClone.Services.StaticDatas;
using ArkheroClone.UI.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly BaseStateMachine _gameStateMachine;
        private readonly DiContainer _projectContainer;

        public BootstrapState(ICoroutineRunner coroutineRunner, BaseStateMachine gameStateMachine, DiContainer projectContainer)
        {
            _coroutineRunner = coroutineRunner;
            _gameStateMachine = gameStateMachine;
            _projectContainer = projectContainer;
        }

        public void Enter()
        {
            RegisterCourutineRunner();
            RegisterSceneLoaderService();
            RegisterBundleProvider();
            RegisterStaticDataServices();
            RegisterInputs();
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void RegisterCourutineRunner()
        {
            _projectContainer.RegisterInstance(_coroutineRunner);
        }

        private void RegisterSceneLoaderService()
        {
            _projectContainer.Register<SceneLoader>
                (registrationType: DiRegistrationType.AsTransient,
                factory: c => new SceneLoader(_projectContainer.Resolve<ICoroutineRunner>()));
        }

        private void RegisterStaticDataServices()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticDatas();
            _projectContainer.RegisterInstance(staticDataService);
        }

        private void RegisterBundleProvider()
        {
            IBundleProvider assetProvider = new BundleProvider();
            assetProvider.Initialize();
            _projectContainer.RegisterInstance(assetProvider);
        }

        private void RegisterInputs()
        {
            RegisterMobileInputService();  
        }

        private void RegisterMobileInputService()
        {
            InputService inputService = new MobileInputService();
            _projectContainer.RegisterInstance(inputService);
        }

        private void RegisterDesktopInputService()
        {
            _projectContainer.Register<InputService>(
                DiRegistrationType.AsTransient,
                c => new DesktopInputService());
        }

        public void Exit()
        {

        }
    }
}