using ArkheroClone.Datas;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Infrastructure.Factory;
using ArkheroClone.Services;
using ArkheroClone.Services.DI;
using ArkheroClone.StaticDatas;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public sealed class CharacterSpawner
    {
        private readonly AbstractFactory<Character> _factory;
        private readonly IStaticDataService _staticDataService;
        private readonly IBundleProvider _bundleProvider;
        private readonly InputService _inputService;
        private readonly CurrencyLevelData _currencyLevelData;
        private readonly DiContainer _container;

        public CharacterSpawner(DiContainer container)
        {
            _currencyLevelData = container.Resolve<CurrencyLevelData>();
            _inputService = container.Resolve<InputService>();
            _staticDataService = container.Resolve<IStaticDataService>();
            _bundleProvider = container.Resolve<IBundleProvider>();
            _factory = new AbstractFactory<Character>(_bundleProvider);
            _container = container;
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
            _currencyLevelData.AddCharacter(hero);
            return hero;
        }

        private void DespawnCharacter(Character character)
        {
            character.OnDespawn -= DespawnCharacter;
            _currencyLevelData.RemoveCharacter(character);
            GameObject.Destroy(character.gameObject);
        }
    }
}