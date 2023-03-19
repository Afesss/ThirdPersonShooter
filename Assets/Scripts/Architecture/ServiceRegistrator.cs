using Architecture.Services;
using UnityEngine;

namespace Architecture
{
    public class ServiceRegistrator : MonoBehaviour
    {
        [SerializeField] private Curtain curtain;

        public Curtain Curtain => curtain;
        public AssetProvider AssetProvider { get; private set; }
        public GameFactory GameFactory { get; private set; }
        public SceneLoader SceneLoader { get; private set; }
        public GameData GameData { get; private set; }

        public void Initialize()
        {
            SceneLoader = new SceneLoader();
            GameData = new GameData();
            AssetProvider = new AssetProvider();
            GameFactory = new GameFactory(AssetProvider, GameData, SceneLoader, Curtain);
        }
    }
}
