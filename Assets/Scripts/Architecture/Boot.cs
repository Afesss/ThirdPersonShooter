using UnityEngine;
using Zenject;

namespace Architecture
{
    public class Boot : MonoInstaller
    {
        [SerializeField] private ServiceRegistrator serviceRegistrator;
        
        public override void InstallBindings()
        {
            Container.BindInstance(serviceRegistrator).AsSingle().NonLazy();
            serviceRegistrator.Initialize();
        }
    }
}