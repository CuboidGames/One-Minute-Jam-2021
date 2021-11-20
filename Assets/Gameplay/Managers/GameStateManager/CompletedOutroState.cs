using System.Threading.Tasks;

namespace Gameplay.Managers.GameStateManager
{
    public class CompletedOutroState : GameState
    {
        public override async Task OnEnter()
        {
            await sceneTransitionManager.FadeIn();
            sceneTransitionManager.LoadScene("IntroScene");
        }

        public override GameState Transition(GameStateEnum state)
        {
            if (state == GameStateEnum.Initializing)
            {
                return new InitializingState();
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

        public override void Update()
        {
            // noop
        }
    }
}