﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Puzzles.Pairs
{
    public class PairsPuzzleController : MonoBehaviour
    {
        [SerializeField] private PairButton[] _pairObjects;
        [SerializeField] private Transform _errorLight;
        [SerializeField] private Transform _successLight;

        private PairColor[] _colors = { PairColor.RED, PairColor.BLUE, PairColor.GREEN, PairColor.YELLOW };

        private PairButton _lastButton = null;

        private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);

        private void Awake()
        {
            if (_colors.Length * 2 != _pairObjects.Length)
            {
                throw new System.Exception("Number of colors has to be half of the number of objects");
            }


            _pairObjects = _pairObjects.OrderBy(x => Random.Range(-1, 2)).ToArray();
        }

        private void Start()
        {
            for (var i = 0; i < _pairObjects.Length; i++)
            {
                _pairObjects[i].SetColor(_colors[(int)(i / 2)]);
                _pairObjects[i].OnPairButtonRevealed += PairButtonRevealed;
            }
        }

        private void PairButtonRevealed(PairButton pairButton)
        {
            if (!_lastButton) {
                _lastButton = pairButton;
                return;
            }

            if (_lastButton.color != pairButton.color)
            {
                StartCoroutine(ConcealAllButtons());
                _lastButton = null;
                return;
            }

            _lastButton = null;
            SetPuzzleState();
        }

        private IEnumerator ConcealAllButtons()
        {
            yield return _waitForSeconds;

            for (var i = 0; i < _pairObjects.Length; i++)
            {
                _pairObjects[i].Conceal();
            }

            SetPuzzleState();
        }

        private void SetPuzzleState()
        {
            var isPuzzleSolved = true;

            for (var i = 0; i < _pairObjects.Length; i++)
            {
                if (!_pairObjects[i].isRevealed)
                {
                    isPuzzleSolved = false;
                    break;
                }
            }

            if (isPuzzleSolved)
            {
                _successLight.gameObject.SetActive(true);
                _errorLight.gameObject.SetActive(false);
            }
            else
            {
                _successLight.gameObject.SetActive(false);
                _errorLight.gameObject.SetActive(true);
            }
        }
    }
}