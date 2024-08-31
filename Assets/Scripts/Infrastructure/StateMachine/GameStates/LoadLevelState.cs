using ArkheroClone.Datas;
using ArkheroClone.Gameplay.Characters;
using ArkheroClone.Gameplay.Platforms;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Services.DI;
using ArkheroClone.Services.SceneLoader;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<LevelPayload>
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
            RegisterCharacterSpawner();
            CreateCurrencyLevelData();
            List<UniTask> tasks = new List<UniTask>()
            {
                SpawnPlatformsAsync(),
                SpawnPlayer()
            };
            await UniTask.WhenAll(tasks);
            RebakeNavMeshSurface();
            _gameStateMachine.Enter<GameLoopState, DiContainer>(_sceneContainer);
        }

        public void Exit()
        {
            
        }

        private DiContainer GetSceneContainer()
        {
            GameObject obj = GameObject.FindGameObjectsWithTag("SceneContainer").First();
            SceneInstaller sceneContainer = obj.GetComponent<SceneInstaller>();
            return sceneContainer.GetSceneInstaller(_sessionContainer);
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
            await spawner.CreateHeroAsync(playerSpawnPosition);
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

            _sceneContainer.RegisterInstance<CurrencyLevelData>(currencyLevelData);
        }
    }
}