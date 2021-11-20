using System.Threading.Tasks;

namespace Gameplay.Managers.GameStateManager
{
    public class IntroState : GameState
    {
        public override void Update()
        {
            gameManager.SetGameState(GameStateEnum.Gameplay);
        }

        public override GameState Transition(GameStateEnum state)
        {
            if (state == GameStateEnum.Gameplay)
            {
                return new GameplayState();
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

    }
}