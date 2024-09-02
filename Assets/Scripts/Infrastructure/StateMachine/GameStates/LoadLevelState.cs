using ArkheroClone.Datas;
using ArkheroClone.Gameplay.Characters;
using ArkheroClone.Gameplay.Platforms;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Infrastructure.Factory;
using ArkheroClone.Infrastructure.Timer;
using ArkheroClone.Services.DI;
using ArkheroClone.Services.SceneLoader;
using ArkheroClone.Services.Scores;
using ArkheroClone.UI.Presenter;
using ArkheroClone.UI.View;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public sealed class LoadLevelState : IPayloadedState<LevelPayload>
    {
        private readonly GameStateMachine _gameStateMachine;
        private DiContainer _sessionContainer;
        private DiContainer _sceneContainer;
        private SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(LevelPayload levelData)
        {
            _sessionContainer = new DiContainer(levelData.SessionDiContainer);
            CleanUp();
            LoadLevel(levelData);
        }

        private void LoadLevel(LevelPayload levelPayload)
        {
            _sessionContainer = levelPayload.SessionDiContainer;
            _sceneLoader = _sessionContainer.Resolve<SceneLoader>();
            _sceneLoader.Load(levelPayload.SceneName, OnLoaded);
        }

        private void CleanUp()
        {
            _sessionContainer.Resolve<IBundleProvider>().CleanUp();
        }

        private async void OnLoaded()
        {
            _sceneContainer = GetSceneContainer();
            CreateCurrencyLevelData();
            RegisterCharacterSpawner();
            RegisterCustomTimer();
            List<UniTask> tasks = new List<UniTask>()
            {
                SpawnPlatformsAsync(),
                SpawnPlayer(),
                SpawnEnemies()
            };
            await InstantiateHUDAsync();
            await UniTask.WhenAll(tasks);
            RebakeNavMeshSurface();
            _gameStateMachine.Enter<GameLoopState, DiContainer>(_sceneContainer);
        }

        public void Exit()
        {
            
        }

        private void RegisterCustomTimer()
        {
            CustomTimer customTimer = new CustomTimer();
            _sceneContainer.RegisterInstance<CustomTimer>(customTimer);
        }

        private async UniTask InstantiateHUDAsync()
        {
            Factory<HUDRoot> hudFactory = new Factory<HUDRoot>
                (
                bundleProvider: _sceneContainer.Resolve<IBundleProvider>(),
                bundlePath: BundlePath.HUD
                );

            HUDRoot hud = await hudFactory.CreateAsync();
            HUDPresenter hUDPresenter = new HUDPresenter
                (hud, 
                _sceneContainer.Resolve<ScoreSystem>(),
                _sceneContainer.Resolve<Player>().Health,
                _sceneContainer.Resolve<CustomTimer>(),
                _sceneContainer.Resolve<InputService>());
        }

        private DiContainer GetSceneContainer()
        {
            GameObject obj = GameObject.FindGameObjectsWithTag("SceneContainer").First();
            SceneInstaller sceneContainer = obj.GetComponent<SceneInstaller>();
            return sceneContainer.GetSceneInstaller(_sessionContainer);
        }

        private async UniTask SpawnEnemies()
        {
            CharacterSpawner spawner = _sceneContainer.Resolve<CharacterSpawner>();
            List<Transform> spawnPoints = _sceneContainer.Resolve<List<Transform>>("EnemiesSpawnPoints");

            foreach (Transform spawnPoint in spawnPoints)
            {
                await spawner.CreateEnemyAsync(EnemyType.FlyingEnemy, spawnPoint.position);
            }
        }

        private void RegisterCharacterSpawner()
        {
            CharacterSpawner characterSpawner = new(_sceneContainer);
            _sceneContainer.RegisterInstance(characterSpawner);
        }

        private async UniTask SpawnPlayer()
        {
            CharacterSpawner spawner = _sceneContainer.Resolve<CharacterSpawner>();
            Vector3 playerSpawnPosition = _sceneContainer.Resolve<Vector3>("PlayerSpawnPosition");
            Player player = await spawner.CreateHeroAsync(playerSpawnPosition);
            _sceneContainer.RegisterInstance(player);
        }

        private async UniTask SpawnPlatformsAsync()
        {
            List<AssetReference> refs = _sceneContainer.Resolve<List<AssetReference>>("PlatformAssetRefs");
            List<Transform> points = _sceneContainer.Resolve<List<Transform>>("PlatformSpawnPoints");
            PlatformSpawner spawner = _sceneContainer.Resolve<PlatformSpawner>();
            await spawner.CreateRandomPlatformsAsync(points, refs);
        }

        private void RebakeNavMeshSurface()
            => _sceneContainer.Resolve<NavMeshSurface>().BuildNavMesh();

        private void CreateCurrencyLevelData()
        {
            CurrencyLevelData currencyLevelData = new CurrencyLevelData();

            _sceneContainer.RegisterInstance(currencyLevelData);
        }
    }
}