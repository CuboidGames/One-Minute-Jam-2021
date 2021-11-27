using System.Threading.Tasks;

namespace Gameplay.Managers.GameStateManager
{
    public abstract class GameState
    {
        protected GameManager gameManager;
        protected SceneTransitionManager sceneTransitionManager;

        public GameState()
        {
            this.gameManager = GameManager.Instance;
            this.sceneTransitionManager = SceneTransitionManager.Instance;
        }

        public virtual async Task OnEnter()
        {
            await Task.Yield();
        }

        public virtual async Task OnExit()
        {
            await Task.Yield();
        }

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