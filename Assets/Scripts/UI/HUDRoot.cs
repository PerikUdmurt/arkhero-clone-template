using TMPro;
using UnityEngine;

namespace ArkheroClone.UI.View
{
    public class HUDRoot : MonoBehaviour
    {
        [SerializeField]
        private Joystick _joystick;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        private TextMeshProUGUI _healthText;

        [SerializeField]
        private TextMeshProUGUI _timerText;

        public Joystick GetJoystick() => _joystick;

        public void SetScore(string value)
            => _scoreText.text = value;

        public void SetHealthText(string value)
            => _healthText.text = value;

        public void SetTimerText(string value)
            => _timerText.text = value;

        public void SetActiveTimer(bool value)
            => _timerText.alpha = value ? 1 : 0;
    }
}