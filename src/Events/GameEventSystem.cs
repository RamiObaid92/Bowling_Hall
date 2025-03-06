using Bowling_Hall.src.Interfaces;

namespace Bowling_Hall.src.Events
{
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

        public void triggerGameStarted()
        {
            foreach (var listener in _listeners)
            {
                listener.onGameStarted();
            }
        }

        public void triggerGameEnded(string winner)
        {
            foreach (var listener in _listeners)
            {
                listener.onGameEnded(winner);
            }
        }

        public void triggerScoreUpdated(string player, int score)
        {
            foreach (var listener in _listeners)
            {
                listener.onScoreUpdated(player, score);
            }
        }
    }
}
