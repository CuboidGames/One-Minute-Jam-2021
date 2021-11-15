namespace Gameplay.Managers.GameStateManager
{
    public class IntroState : GameState
    {
        public IntroState(GameManager gameManager) : base(gameManager) { }

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
            gameManager.SetGameState(GameStateEnum.Gameplay);
        }

        public override GameState Transition(GameStateEnum state)
        {
            if (state == GameStateEnum.Gameplay)
            {
                return new GameplayState(gameManager);
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

    }
}