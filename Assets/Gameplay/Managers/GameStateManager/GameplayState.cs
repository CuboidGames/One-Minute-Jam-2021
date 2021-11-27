using System.Threading.Tasks;

namespace Gameplay.Managers.GameStateManager
{
    public class GameplayState : GameState
    {
        public override async Task OnEnter()
        {
            gameManager.EnablePlayer();
            gameManager.StartGame();
            await sceneTransitionManager.FadeOut();
        }

        public override async Task OnExit()
        {
            gameManager.DisablePlayer();
            gameManager.EndGame();
            await Task.CompletedTask;
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
                    return new CompletedOutroState();
                case GameStateEnum.FailedOutro:
                    return new FailedOutroState();
            }


            throw new System.Exception($"Transition from {this.GetType().Name} to {state} not allowed");
        }

        private bool IsGameFailed()
        {
            return gameManager.currentGameProgress >= 1f;
        }


        private bool IsGameCompleted()
        {
            return gameManager.exitColliderTouched;
        }
    }
}