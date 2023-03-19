using System;
using Architecture.Services;
using Opsive.Shared.Input;
using Opsive.UltimateCharacterController.Items.Actions;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace Hero
{
    public class HeroState : MonoBehaviour
    {
        [SerializeField] private AttributeManager attributeManager;
        [SerializeField] private ShootableWeapon shootableWeapon;
        [SerializeField] private Animator animator;
        [SerializeField] private UnityInput unityInput;
        [SerializeField] private MonoBehaviour[] allBehaviours;

        private readonly int _dieCash = Animator.StringToHash("Die");

        private GameData _gameData;

        public void Initialize(GameData gameData)
        {
            _gameData = gameData;
            _gameData.HeroDie = false;
            _gameData.MaxHeroHealth = attributeManager.Attributes[0].MaxValue;
            _gameData.CurrentHeroHealth = attributeManager.Attributes[0].Value;
            _gameData.AmmoCount = shootableWeapon.ClipRemaining;
        }

        public void InvokeDie()
        {
            unityInput.DisableCursor = false;
            foreach (MonoBehaviour behaviour in allBehaviours)
            {
                behaviour.enabled = false;
            }

            animator.SetTrigger(_dieCash);
            _gameData.HeroDie = true;
        }

        private void Update()
        {
            _gameData.CurrentHeroHealth = attributeManager.Attributes[0].Value;
            _gameData.AmmoCount = shootableWeapon.ClipRemaining;
        }
    }
}
