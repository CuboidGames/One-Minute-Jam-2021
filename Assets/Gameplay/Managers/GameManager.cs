using System;
using System.Collections.Generic;
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

        public event Action OnGameStart;

        public event Action OnGameComplete;
        public event Action OnGameTimeout;

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
            _currentGameTime = Time.time - _gameStartTime;

            if (currentGameProgress >= 1f)
            {
                EndGame();
            }

            bool allPuzzlesResolved = true;

            foreach (var puzzle in _puzzles)
            {
                if (!puzzle.IsResolved)
                {
                    allPuzzlesResolved = false;
                    break;
                }
            }

            if (allPuzzlesResolved)
            {
                EndGame();
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
            StartGame();
        }

        private void StartGame()
        {
            _currentGameTime = 0f;
            _gameStartTime = Time.time;

            if (OnGameStart != null)
            {
                OnGameStart.Invoke();
            }
        }

        private void EndGame()
        {
            Debug.Log("Game ended");

            if (currentGameTime >= _gameDuration)
            {
                Debug.Log("Game Over");
            }
            else
            {
                Debug.Log("Game completed");
            }

            if (OnGameTimeout != null)
            {
                OnGameTimeout.Invoke();
            }
        }
    }
}