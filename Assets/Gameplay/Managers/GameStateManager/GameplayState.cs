namespace Gameplay.Managers.GameStateManager
{
    public class GameplayState : GameState
    {
        public GameplayState(GameManager gameManager) : base(gameManager) { }

        public override void OnEnter()
        {
            gameManager.StartGame();
        }

        public override void OnExit()
        {
            // noop
        }

        public override void Update()
        {
            gameManager.UpdateGameProgress();

            if (IsGameFailed())
            {
                gameManager.SetGameState(GameStateEnum.FailedOutro);
            }

            if (IsGameCompleted())
            {
                gameManager.SetGameState(GameStateEnum.CompletedOutro);
            }
        }

        public override GameState Transition(GameStateEnum state)
        {
            switch (state)
            {
                case GameStateEnum.CompletedOutro:
                    return new CompletedOutroState(gameManager);
                case GameStateEnum.FailedOutro:
                    return new FailedOutroState(gameManager);
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

        private bool IsGameFailed()
        {
            return gameManager.currentGameProgress >= 1f;
        }


        private bool IsGameCompleted()
        {
            foreach (var puzzle in gameManager.puzzles)
            {
                if (!puzzle.IsResolved)
                {
                    return false;
                }
            }

            // check also that player is standing in the exit platform

            return true;
        }
    }
}