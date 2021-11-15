using System;
using System.Collections.Generic;
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

        public List<BasePuzzle> puzzles {  get { return _puzzles; } }
        private GameState gameState;

        public event Action OnGameStart;

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

        private void Update()
        {
            if (gameState != null) {
                gameState.Update();
            }

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
        }

        private void Start()
        {
            SetInitialState();
        }

        public void StartGame()
        {
            _currentGameTime = 0f;
            _gameStartTime = Time.time;

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

        private void EndGame()
        {
            if (OnGameEnd != null)
            {
                OnGameEnd.Invoke();
            }
        }

        public void SetInitialState() {
            if (gameState != null)
            {
                gameState.Transition(GameStateEnum.Initializing);
                return;
            }

            gameState = new InitializingState(this);
            gameState.OnEnter();
        }

        public void SetGameState(GameStateEnum state)
        {
            if (gameState == null)
            {
                SetInitialState();
            }

            gameState.OnExit();
            gameState = gameState.Transition(state);
            gameState.OnEnter();
        }
    }
}