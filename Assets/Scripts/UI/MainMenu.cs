using System;
using Architecture;
using Architecture.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        [Inject] private ServiceRegistrator _services;
        private void Start()
        {
            playButton.onClick.AddListener(PlayClick);
        }

        private void PlayClick()
        {
            _services.Curtain.ShowCurtain();
            _services.SceneLoader.LoadScene(SceneLoader.GameScene, _services.Curtain.HideCurtain);
        }
    }
}
