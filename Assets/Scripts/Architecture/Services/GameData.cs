using System;

namespace Architecture.Services
{
    public class GameData
    {
        public event Action<float> HeroHealthChanged;
        public event Action<int> AmmoCountChanged;
        public event Action<int> DieZombieValueChanged;
        public event Action HeroDied;

        public bool HeroDie
        {
            get => _heroDie;
            set
            {
                _heroDie = value;
                if(_heroDie)
                    HeroDied?.Invoke();
            }
        }
        
        public float MaxHeroHealth { get; set; }

        public int ZombieDieValue { get; set; }

        public float CurrentHeroHealth
        {
            get => _currentHeroHealth;
            set
            {
                if (_currentHeroHealth != value)
                {
                    _currentHeroHealth = value;
                    HeroHealthChanged?.Invoke(_currentHeroHealth);
                }
            }
        }

        public int AmmoCount
        {
            get => _ammoCount;
            set
            {
                if (_ammoCount != value)
                {
                    _ammoCount = value;
                    AmmoCountChanged?.Invoke(_ammoCount);
                }
            }
        }

        private bool _heroDie;
        private int _ammoCount;
        private float _currentHeroHealth;

        public void IncreaseDieZombieValue()
        {
            ZombieDieValue++;
            DieZombieValueChanged?.Invoke(ZombieDieValue);
        }
    }
}
