namespace Bowling_Hall.src.Interfaces
{
    public interface IGameEventListener
    {
        void onGameStarted();
        void onGameEnded(string winner);
        void onScoreUpdated(string player, int score);

    }
}
