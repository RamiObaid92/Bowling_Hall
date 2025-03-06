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
    }
}
