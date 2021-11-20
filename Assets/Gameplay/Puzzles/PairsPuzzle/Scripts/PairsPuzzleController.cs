using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Puzzles.Base;
using UnityEngine;

namespace Gameplay.Puzzles.Pairs
{
    public class PairsPuzzleController : BasePuzzle
    {
        [SerializeField] private PairButton[] _pairObjects;

        private PairColor[] _colors = { PairColor.RED, PairColor.BLUE, PairColor.GREEN, PairColor.YELLOW };

        private PairButton _lastButton = null;

        private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);

        private new void Awake()
        {
            base.Awake();

            if (_colors.Length * 2 != _pairObjects.Length)
            {
                throw new System.Exception("Number of colors has to be half of the number of objects");
            }

        }

        private void Start()
        {
            for (var i = 0; i < _pairObjects.Length; i++)
            {
                _pairObjects[i].OnPairButtonRevealed += PairButtonRevealed;
            }
        }

        private void PairButtonRevealed(PairButton pairButton)
        {
            if (!_lastButton) {
                _lastButton = pairButton;
                pairButton.PlaySuccess();
                return;
            }

            if (_lastButton.color != pairButton.color)
            {
                pairButton.PlayError();
                StartCoroutine(ConcealAllButtons());
                _lastButton = null;
                return;
            }

            pairButton.PlaySuccess();
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

            foreach (var pairButton in _pairObjects)
            {
                if (!pairButton.isRevealed)
                {
                    isPuzzleSolved = false;
                    break;
                }
            }

            SetResolved(isPuzzleSolved);
        }

        public override void Init()
        {
            _pairObjects = _pairObjects.OrderBy(x => Random.Range(-1, 2)).ToArray();

            for (var i = 0; i < _pairObjects.Length; i++)
            {
                _pairObjects[i].SetColor(_colors[(int)(i / 2)]);
                _pairObjects[i].Conceal();
            }

            SetResolved(false);
        }
    }
}