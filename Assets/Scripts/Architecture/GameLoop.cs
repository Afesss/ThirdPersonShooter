using System.Collections;
using Architecture.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Architecture
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Transform heroSpawnPoint;
        [SerializeField] private Transform[] zombieSpawnPoints;
        [SerializeField] private float zombieSpawnDuration = 1;

        private const float _minDistanceToHero = 5;
        private GameFactory _gameFactory;
        private GameData _gameData;
        
        [Inject]
        public void Construct(ServiceRegistrator services)
        {
            _gameFactory = services.GameFactory;
            _gameData = services.GameData;
        }

        private void Start()
        {
            _gameData.ZombieDieValue = 0;
            Spawn();
        }

        private void Spawn()
        {
            _gameFactory.CreateHero(heroSpawnPoint.position);
            _gameFactory.CreateHud();
            StartCoroutine(SpawnZombie());
        }
        

        private IEnumerator SpawnZombie()
        {
            while (!_gameData.HeroDie)
            {
                _gameFactory.CreateZombie(GetZombieSpawnPos());
                yield return new WaitForSeconds(zombieSpawnDuration);
            }
        }

        private Vector3 GetZombieSpawnPos()
        {
            int randomNum = Random.Range(0, zombieSpawnPoints.Length);
            Vector3 pos = zombieSpawnPoints[randomNum].position;
            if ((pos - _gameFactory.Hero.transform.position).magnitude < _minDistanceToHero)
            {
                pos = GetZombieSpawnPos();
            }

            return pos;
        }
    }
}
