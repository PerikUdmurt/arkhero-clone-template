using System;

namespace ArkheroClone.Services.Scores
{
    public class ScoreSystem
    {
        private int _totalScore;

        public ScoreSystem(int initialScore = 0)
        {
            _totalScore = initialScore;
        }

        public int Score { get => _totalScore; }
        
        public event Action<int> ScoreChanged;

        public int GetScore() => _totalScore;

        public void AddScore(int score)
        {
            _totalScore += score;
            ScoreChanged?.Invoke(score);
        }

        public void ReduceScore(int score)
        {
            _totalScore -= score;
            ScoreChanged?.Invoke(score);
        }
    }
}