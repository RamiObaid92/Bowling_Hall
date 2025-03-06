namespace Bowling_Hall.src.Interfaces
{
    // Observer Interface
    public interface IGameEventListener
    {
        void onGameStarted();
        void onGameEnded(string winner, int score);
        void onScoreUpdated(string player, int score);
    }
}
