using ArkheroClone.Services.DI;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

public class SceneInstaller : MonoBehaviour
{
    [SerializeField]
    private List<AssetReference> _platformAssetRefs;

    [SerializeField]
    private List<Transform> _platformSpawnPoints;

    [SerializeField]
    private NavMeshSurface _navMeshSurface;

    [SerializeField]
    private Vector3 _playerSpawnPosition;

    private DiContainer _sceneContainer;
    public DiContainer GetSceneInstaller(DiContainer projectContainer)
    {
        _sceneContainer = new DiContainer(projectContainer);
        RegisterInitConfigs();
        return _sceneContainer;
    }

    public void RegisterInitConfigs()
    {
        _sceneContainer.RegisterInstance(_platformAssetRefs, "PlatformAssetRefs");
        _sceneContainer.RegisterInstance(_platformSpawnPoints, "PlatformSpawnPoints");
        _sceneContainer.RegisterInstance(_navMeshSurface);
        _sceneContainer.RegisterInstance(_playerSpawnPosition, "PlayerSpawnPosition");
    }
}
