namespace Gameplay.Managers.GameStateManager
{
    public class InitializingState : GameState
    {
        public override void Update()
        {
            gameManager.SetGameState(GameStateEnum.Intro);
        }

        public override GameState Transition(GameStateEnum state)
        {
            if (state == GameStateEnum.Intro)
            {
                return new IntroState();
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

    }
}