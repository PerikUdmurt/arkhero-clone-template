using ArkheroClone.Gameplay.Characters;
using ArkheroClone.Infrastructure.Timer;
using ArkheroClone.Services.Scores;
using ArkheroClone.UI.View;

namespace ArkheroClone.UI.Presenter
{
    public sealed class HUDPresenter
    {
        private readonly HUDRoot _hudRoot;
        private readonly CustomTimer _startGameTimer;
        private ScoreSystem _scoreSystem;
        private Health _playerHealth;

        public HUDPresenter(HUDRoot hudRoot, ScoreSystem scoreSystem, Health playerHealth, CustomTimer startGameTimer, InputService iputService) 
            : this(hudRoot, scoreSystem, playerHealth, startGameTimer)
        {
            Joystick joystick = _hudRoot.GetJoystick();
            
            if (iputService is MobileInputService)
            {
                MobileInputService input = iputService as MobileInputService;
                input.ProvideJoystick(joystick);
            }
        }

        public HUDPresenter(HUDRoot hudRoot, ScoreSystem scoreSystem, Health playerHealth, CustomTimer startGameTimer)
        {
            _hudRoot = hudRoot;
            _startGameTimer = startGameTimer;
            _startGameTimer.CountdownChanged += ShowTimer;
            BindScoreSystem(scoreSystem);
            BindHealthBar(playerHealth);
        }

        private void BindHealthBar(Health playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.HealthChanged += UpdateHealth;
            UpdateHealth(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
        }

        private void BindScoreSystem(ScoreSystem scoreSystem)
        {
            _scoreSystem = scoreSystem;
            _scoreSystem.ScoreChanged += UpdateScoreText;
            UpdateScoreText(_scoreSystem.Score);
        }

        private void UpdateScoreText(int score)
            => _hudRoot.SetScore($"Score: {score}");

        private void UpdateHealth(int health, int maxHealth)
            => _hudRoot.SetHealthText($"Health: {health}/{maxHealth}");
        
        private void ShowTimer(int time)
        {
            if (time > 0)
            {
                _hudRoot.SetActiveTimer(true);
                _hudRoot.SetTimerText(time.ToString());
            }
            else
                _hudRoot.SetActiveTimer(false);
        }
    }
}