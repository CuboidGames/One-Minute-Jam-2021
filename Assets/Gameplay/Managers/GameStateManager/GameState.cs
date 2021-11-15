namespace Gameplay.Managers.GameStateManager
{
    public abstract class GameState
    {
        protected GameManager gameManager;

        public GameState(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update();

        public abstract GameState Transition(GameStateEnum state);

    }
    public enum GameStateEnum
    {
        Initializing,
        Intro,
        Gameplay,
        CompletedOutro,
        FailedOutro,
    }
}