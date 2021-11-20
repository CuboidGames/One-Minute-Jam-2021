using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMF;
using Gameplay.Managers.GameStateManager;
using Gameplay.Puzzles.Base;
using UnityEngine;

namespace Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        [SerializeField] private float _gameDuration = 60f;

        private float _gameStartTime = 0f;
        private float _currentGameTime = 0f;
        private List<BasePuzzle> _puzzles = new List<BasePuzzle>();

        public List<BasePuzzle> puzzles { get { return _puzzles; } }
        private GameState gameState;

        public event Action OnGameStart;

        public bool exitColliderTouched = false;

        public event Action OnGameComplete;
        public event Action OnGameEnd;

        public float currentGameTime
        {
            get { return _currentGameTime; }
        }

        public float currentGameProgress
        {
            get { return _currentGameTime / _gameDuration; }
        }

        [SerializeField] private Transform playerStartingPosition;

        [SerializeField] private AdvancedWalkerController playerWalkerController;
        [SerializeField] private CameraController playerCameraController;

        private AudioControl _playerAudioSource;

        private void Update()
        {
            if (gameState != null)
            {
                gameState.Update();
            }

            UpdateGameProgress();
        }

        private void Awake()
        {
            Debug.Log("Game manager initialized");

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            var puzzleGameObjects = GameObject.FindGameObjectsWithTag("Puzzle");

            for (var i = 0; i < puzzleGameObjects.Length; i++)
            {
                _puzzles.Add(puzzleGameObjects[i].GetComponent<BasePuzzle>());
            }

            _playerAudioSource = playerWalkerController.GetComponent<AudioControl>();
        }

        private async void Start()
        {
            await SetInitialState();
        }

        public void EnablePlayer()
        {
            _playerAudioSource.audioClipVolume = 0.2f;
            playerWalkerController.transform.position = playerStartingPosition.position;
            playerCameraController.SetRotationAngles(playerStartingPosition.rotation.eulerAngles.x, playerStartingPosition.rotation.eulerAngles.y);
            playerWalkerController.enabled = true;
            playerCameraController.enabled = true;
        }

        public void DisablePlayer()
        {
            _playerAudioSource.audioClipVolume = 0f;
            playerWalkerController.enabled = false;
            playerCameraController.enabled = false;
        }

        public void StartGame()
        {
            _currentGameTime = 0f;
            _gameStartTime = Time.time;
            exitColliderTouched = false;
            
            foreach (var puzzle in _puzzles)
            {
                puzzle.Init();
            }

            if (OnGameStart != null)
            {
                OnGameStart.Invoke();
            }
        }

        public void UpdateGameProgress()
        {
            // TODO: Use Time.deltaTime instead to handle states such as pause, animations, etc.
            _currentGameTime = Time.time - _gameStartTime;

        }

        public void EndGame()
        {
            if (OnGameEnd != null)
            {
                OnGameEnd.Invoke();
            }
        }

        public async Task SetInitialState()
        {
            if (gameState != null)
            {
                gameState.Transition(GameStateEnum.Initializing);

                await Task.CompletedTask;
                return;
            }

            gameState = new InitializingState();
            await gameState.OnEnter();
        }

        public async Task SetGameState(GameStateEnum state)
        {
            if (gameState == null)
            {
                await SetInitialState();
            }

            Debug.Log("Current state: " + gameState.GetType().Name);
            Debug.Log("Next state: " + state);


            await gameState.OnExit();
            gameState = gameState.Transition(state);
            await gameState.OnEnter();
        }
    }
}