using Bowling_Hall.src.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bowling_Hall.src.Services
{
    // Observer Klass
    public class ScoreLogger : IGameEventListener
    {
        private readonly ILogger<ScoreLogger> _logger;

        public ScoreLogger(ILogger<ScoreLogger> logger)
        {
            _logger = logger;
        }

        public void onGameStarted()
        {
            _logger.LogInformation($"Spel startades");
        }

        public void onGameEnded(string winner, int score)
        {
            _logger.LogInformation($"Spelet avslutat, vinnare är {winner} med {score} poäng");
        }

        public void onScoreUpdated(string player, int score)
        {
            _logger.LogInformation($"{player} har fått {score} poäng");
        }
    }
}
