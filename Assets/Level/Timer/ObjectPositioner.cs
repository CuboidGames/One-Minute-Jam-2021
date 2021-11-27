using System;
using Gameplay.Managers;
using UnityEngine;

namespace Level.Timer
{
    public class ObjectPositioner : MonoBehaviour
    {
        [SerializeField] private float _transformRadius;

        private GameManager _gameManager;
        private Vector3 _initialPosition;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _initialPosition = transform.localPosition;
        }

        private void Update()
        {
            var gameProgress = Mathf.Lerp(0, Mathf.PI * 2, _gameManager.currentGameProgress);

            transform.localPosition = _initialPosition + new Vector3(
                Mathf.Sin(gameProgress) * _transformRadius,
                Mathf.Cos(gameProgress) * _transformRadius,
                0
            );
        }
    }
}