using System.Threading.Tasks;

namespace Gameplay.Managers.GameStateManager
{
    public class FailedOutroState : GameState
    {
        public override async Task OnEnter()
        {
            await sceneTransitionManager.FadeIn();
            gameManager.SetGameState(GameStateEnum.Initializing);
        }

        public override void Update()
        {
            // noop
        }

        public override GameState Transition(GameStateEnum state)
        {
            if (state == GameStateEnum.Initializing)
            {
                return new InitializingState();
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

    }
}