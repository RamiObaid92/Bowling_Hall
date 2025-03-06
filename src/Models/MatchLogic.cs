using Bowling_Hall.src.Events;

namespace Bowling_Hall.src.Models
{
    public class MatchLogic
    {
        private readonly GameEventSystem _eventSystem;
        private readonly string _playerOne;
        private readonly string _playerTwo;

        private static readonly Random _random = new();

        private readonly Dictionary<string, List<int>> _scores = new();

        public MatchLogic(GameEventSystem eventSystem, string playerOne, string playerTwo)
        {
            _eventSystem = eventSystem;
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

        public void PlayMatch()
        {
            _eventSystem.TriggerGameStarted();

            _scores[_playerOne] = new List<int>();
            _scores[_playerTwo] = new List<int>();

            SimulatePlayer(_playerOne);
            SimulatePlayer(_playerTwo);

            int playerOneScore = CalcScore(_scores[_playerOne]);
            int playerTwoScore = CalcScore(_scores[_playerTwo]);
            string winner = playerOneScore > playerTwoScore ? _playerOne : _playerTwo;

            _eventSystem.TriggerGameEnded(winner);
        }

        private void SimulatePlayer(string player)
        {
            _scores[player] = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                int firstRoll = _random.Next(0, 11);
                int secondRoll = (firstRoll == 10) ? 0 : _random.Next(0, 11 - firstRoll);

                _scores[player].Add(firstRoll);
                _scores[player].Add(secondRoll);

                int currentScore = CalcScore(_scores[player]);
                _eventSystem.TriggerScoreUpdated(player, currentScore);
            }
        }

        private int CalcScore(List<int> rolls)
        {
            int score = 0;
            int rollIndex = 0;

            for (int i = 1; i <= 10; i++)
            {
                if (rolls[rollIndex] == 10)
                {
                    score += 10 + rolls[rollIndex + 1] + rolls[rollIndex + 2];
                    rollIndex+= 1;
                }
                else if (rolls[rollIndex] + rolls[rollIndex + 1] == 10)
                {
                    score += 10 + rolls[rollIndex + 2];
                    rollIndex += 2;
                }
                else
                {
                    score += rolls[rollIndex] + rolls[rollIndex + 1];
                    rollIndex += 2;
                }
            }
            return score;
        }
    }
}
