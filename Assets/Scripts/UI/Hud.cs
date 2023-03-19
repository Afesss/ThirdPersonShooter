using System;
using Architecture.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private TMP_Text ammoText;
        [SerializeField] private GameObject zombiePanel;
        [SerializeField] private TMP_Text zombieText;
        [Header("Game Over Window")]
        [SerializeField] private GameObject gameOverWindow;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button toMenuButton;
        [SerializeField] private TMP_Text zombieFinalText;

        private GameData _gameData;
        private SceneLoader _sceneLoader;
        private Curtain _curtain;
        public void Initialize(GameData gameData, SceneLoader sceneLoader, Curtain curtain)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            
            _gameData.HeroHealthChanged += GameDataOnHeroHealthChanged;
            _gameData.DieZombieValueChanged += GameDataOnDieZombieValueChanged;
            _gameData.AmmoCountChanged += GameDataOnAmmoCountChanged;
            _gameData.HeroDied += GameDataOnHeroDied;
            gameOverWindow.SetActive(false);
            SubscribeButtons();
        }

        private void OnDestroy()
        {
            _gameData.HeroHealthChanged -= GameDataOnHeroHealthChanged;
            _gameData.DieZombieValueChanged -= GameDataOnDieZombieValueChanged;
            _gameData.AmmoCountChanged -= GameDataOnAmmoCountChanged;
            _gameData.HeroDied -= GameDataOnHeroDied;
            restartButton.onClick.RemoveAllListeners();
            toMenuButton.onClick.RemoveAllListeners();
        }

        private void GameDataOnHeroDied()
        {
            zombiePanel.SetActive(false);
            gameOverWindow.SetActive(true);
            zombieFinalText.text = _gameData.ZombieDieValue.ToString();
        }

        private void SubscribeButtons()
        {
            restartButton.onClick.AddListener(() =>
            {
                _curtain.ShowCurtain();
                _sceneLoader.LoadScene(SceneLoader.GameScene, _curtain.HideCurtain);
            });
            
            toMenuButton.onClick.AddListener(() =>
            {
                _curtain.ShowCurtain();
                _sceneLoader.LoadScene(SceneLoader.MenuScene, _curtain.HideCurtain);
            });
        }

        private void GameDataOnAmmoCountChanged(int count)
        {
            ammoText.text = $"∞ / {count}";
        }

        private void GameDataOnDieZombieValueChanged(int dieCount)
        {
            zombieText.text = dieCount.ToString();
        }

        private void GameDataOnHeroHealthChanged(float healthCount)
        {
            healthImage.fillAmount = healthCount / _gameData.MaxHeroHealth;
        }
    }
}
