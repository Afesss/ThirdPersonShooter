
using Hero;
using Opsive.UltimateCharacterController.Camera;
using UI;
using UnityEngine;
using Zombie;

namespace Architecture.Services
{
    public class GameFactory
    {
        private const string _heroPath = "Prefabs/Hero";
        private const string _zombiePath = "Prefabs/Zombie";
        private const string _heroCameraPath = "Prefabs/HeroCamera";
        private const string _hudPath = "Prefabs/HUD";
        
        private readonly AssetProvider _assetProvider;
        private readonly GameData _gameData;
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;

        public GameObject Hero { get; private set; }
        
        public GameFactory(AssetProvider assetProvider, GameData gameData, SceneLoader sceneLoader, Curtain curtain)
        {
            _assetProvider = assetProvider;
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void CreateHero(Vector3 position)
        {
            Hero = Object.Instantiate(_assetProvider.LoadObject(_heroPath), position, Quaternion.identity);
            Hero.GetComponent<HeroState>().Initialize(_gameData);
            GameObject camObj = Object.Instantiate(_assetProvider.LoadObject(_heroCameraPath));
            camObj.GetComponent<CameraController>().Character = Hero;
        }

        public void CreateHud()
        {
            var obj = Object.Instantiate(_assetProvider.LoadObject(_hudPath));
            obj.GetComponent<Hud>().Initialize(_gameData, _sceneLoader, _curtain);
        }

        public GameObject CreateZombie(Vector3 position)
        {
            var zombie = Object.Instantiate(_assetProvider.LoadObject(_zombiePath), 
                position, Quaternion.identity);

            var zombieController = zombie.transform.GetComponent<ZombieController>();
            zombieController.Initialize(Hero.transform, _gameData);

            return zombie;
        }
    }
}
