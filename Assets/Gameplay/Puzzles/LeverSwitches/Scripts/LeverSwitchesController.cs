using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Puzzles.LeverSwitches
{
    public class LeverSwitchesController : MonoBehaviour
    {

        [SerializeField] private Transform _puzzleTargetIndicator;
        [SerializeField] private Transform _puzzleValueIndicator;
        [SerializeField] private Transform _errorLight;
        [SerializeField] private Transform _successLight;

        [SerializeField] private LeverSwitch[] _leverSwitches;

        private float _puzzleValue;
        private float _targetPuzzleValue;

        private void Awake()
        {
            // subcribe to leverwitches events
            foreach (LeverSwitch leverSwitch in _leverSwitches)
            {
                leverSwitch.OnLeverSwitchToggled += LeverSwitchToggled;
            }
        }

        private void Start()
        {
            SetTargetPuzzleValue();
            SetPuzzleValue();
        }

        private void Update()
        {
            var targetPosition = new Vector3(Mathf.Lerp(0.15f, -0.15f, _puzzleValue), _puzzleValueIndicator.localPosition.y, _puzzleValueIndicator.localPosition.z);

            _puzzleValueIndicator.localPosition = Vector3.Lerp(_puzzleValueIndicator.localPosition, targetPosition, Time.deltaTime * 10);
        }

        private void LeverSwitchToggled(LeverSwitch toggledLeverSwitch)
        {
            SetPuzzleValue();
        }

        private void SetTargetPuzzleValue()
        {
            float targetValue = 0f;

            while (targetValue <= 0 || targetValue >= 1)
            {
                targetValue = 0f;

                foreach (LeverSwitch leverSwitch in _leverSwitches)
                {
                    if (UnityEngine.Random.value > 0.5f)
                    {
                        targetValue += leverSwitch.onValue;
                    }
                    else
                    {
                        targetValue += leverSwitch.offValue;
                    }
                }
            }

            _targetPuzzleValue = targetValue;

            _puzzleTargetIndicator.localPosition = new Vector3(Mathf.Lerp(0.15f, -0.15f, _targetPuzzleValue), _puzzleTargetIndicator.localPosition.y, _puzzleTargetIndicator.localPosition.z);
        }

        private void SetPuzzleValue()
        {
            float newValue = 0f;

            foreach (LeverSwitch leverSwitch in _leverSwitches)
            {
                newValue = leverSwitch.PipeValueCalculation(newValue);
            }

            _puzzleValue = newValue;

            if (_puzzleValue == _targetPuzzleValue)
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