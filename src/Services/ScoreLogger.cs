using Bowling_Hall.src.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bowling_Hall.src.Services
{
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

        public void onGameEnded(string winner)
        {
            _logger.LogInformation($"Spelet avslutat, vinnare är {winner}");
        }

        public void onScoreUpdated(string player, int score)
        {
            _logger.LogInformation($"{player} har fått {score} poäng");
        }
    }
}
