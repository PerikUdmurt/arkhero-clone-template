using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Infrastructure.Factory;
using ArkheroClone.Services;
using ArkheroClone.Services.DI;
using ArkheroClone.StaticDatas;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public class CharacterSpawner
    {
        public AbstractFactory<Character> _factory;
        public IStaticDataService _staticDataService;
        public IBundleProvider _bundleProvider;
        public InputService _inputService;

        public CharacterSpawner(DiContainer container)
        {
            _inputService = container.Resolve<InputService>();
            _staticDataService = container.Resolve<IStaticDataService>();
            _bundleProvider = container.Resolve<IBundleProvider>();
            _factory = new AbstractFactory<Character>(_bundleProvider);
        }

        public async UniTask<Player> CreateHeroAsync(Vector3 atPosition)
        {
            _staticDataService.GetStaticData("Hero", out HeroStaticData heroStaticData);
            Player hero = await CreateCharacterAsync(atPosition, heroStaticData) as Player;
            Debug.Log(_inputService);
            hero.Construct(heroStaticData, _bundleProvider, _inputService);
            return hero;
        }

        public async UniTask<Enemy> CreateEnemyAsync(EnemyType enemyType,Vector3 atPosition)
        {
            _staticDataService.GetStaticData(enemyType.ToString(), out EnemyStaticData heroStaticData);
            Enemy hero = await CreateCharacterAsync(atPosition, heroStaticData) as Enemy;
            hero.Construct(heroStaticData, _bundleProvider);
            
            return hero;
        }

        private async UniTask<Character> CreateCharacterAsync(Vector3 atPosition, HeroStaticData heroStaticData)
        {
            Character hero = await _factory.CreateAsync(heroStaticData.CharacterAsset);
            hero.transform.position = atPosition;
            hero.OnDespawn += DespawnCharacter;
            return hero;
        }

        private void DespawnCharacter(Character character)
        {
            character.OnDespawn -= DespawnCharacter;
            GameObject.Destroy(character.gameObject);
        }
    }
}