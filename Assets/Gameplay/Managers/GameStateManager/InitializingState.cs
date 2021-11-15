namespace Gameplay.Managers.GameStateManager
{
    public class InitializingState : GameState
    {
        public InitializingState(GameManager gameManager) : base(gameManager) { }

        public override void OnEnter()
        {
            // noop
        }

        public override void OnExit()
        {
            // noop
        }

        public override void Update()
        {
            gameManager.SetGameState(GameStateEnum.Intro);
        }

        public override GameState Transition(GameStateEnum state)
        {
            if (state == GameStateEnum.Intro)
            {
                return new IntroState(gameManager);
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

    }
}