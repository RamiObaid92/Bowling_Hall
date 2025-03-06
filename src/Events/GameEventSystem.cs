using Bowling_Hall.src.Interfaces;

namespace Bowling_Hall.src.Events
{
    // Observer Klass
    public class GameEventSystem
    {
        private readonly List<IGameEventListener> _listeners = new();

        public void AddListener(IGameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(IGameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void TriggerGameStarted()
        {
            foreach (var listener in _listeners)
            {
                listener.onGameStarted();
            }
        }

        public void TriggerGameEnded(string winner, int score)
        {
            foreach (var listener in _listeners)
            {
                listener.onGameEnded(winner, score);
            }
        }

        public void TriggerScoreUpdated(string player, int score)
        {
            foreach (var listener in _listeners)
            {
                listener.onScoreUpdated(player, score);
            }
        }
    }
}
