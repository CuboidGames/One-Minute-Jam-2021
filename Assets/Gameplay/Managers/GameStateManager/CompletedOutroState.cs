namespace Gameplay.Managers.GameStateManager
{
    public class CompletedOutroState : GameState
    {
        public CompletedOutroState(GameManager gameManager) : base(gameManager) { }

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
            // noop
        }

        public override GameState Transition(GameStateEnum state)
        {
            if (state == GameStateEnum.Initializing)
            {
                return new InitializingState(gameManager);
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

    }
}