using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Puzzles.Base;
using System.Linq;

namespace Gameplay.Puzzles.LeverSwitches
{
    public class LeverSwitchesController : BasePuzzle
    {

        [SerializeField] private LedLight[] _puzzleTargetIndicators;
        [SerializeField] private LedLight[] _puzzleValueIndicators;
        [SerializeField] private LeverSwitch[] _leverSwitches;

        private int[] _puzzleValue;
        private int[] _targetPuzzleValue;

        private new void Awake()
        {
            base.Awake();

            foreach (LeverSwitch leverSwitch in _leverSwitches)
            {
                leverSwitch.OnLeverSwitchToggled += LeverSwitchToggled;
            }
        }

        public override void Init()
        {
            foreach (LeverSwitch leverSwitch in _leverSwitches)
            {
                leverSwitch.Init(_puzzleTargetIndicators.Length);
            }

            SetTargetPuzzleValue();
            SetPuzzleValue();
        }

        private void LeverSwitchToggled(LeverSwitch toggledLeverSwitch)
        {
            SetPuzzleValue();
        }

        private void SetTargetPuzzleValue()
        {
            int[] _targetValue = new int[_puzzleTargetIndicators.Length];

            foreach (LeverSwitch leverSwitch in _leverSwitches)
            {
                print("----");
                print("----");
                print("----");
                print("----");
                print("----");
                print("----");
                foreach (int value in _targetValue)
                {
                    print(value);
                }
                if (UnityEngine.Random.value > 0.45f)
                {
                    _targetValue = leverSwitch.PipeValueCalculation(_targetValue);
                }
            }

            _targetPuzzleValue = _targetValue;

            for (var i = 0; i < _puzzleTargetIndicators.Length; i++)
            {
                UpdateLed(_puzzleTargetIndicators[i], _targetValue[i]);
            }
        }

        private void SetPuzzleValue()
        {
            int[] newValue = new int[_puzzleValueIndicators.Length];

            foreach (LeverSwitch leverSwitch in _leverSwitches)
            {
                newValue = leverSwitch.PipeValueCalculationConditional(newValue);
            }

            _puzzleValue = newValue;

            for (var i = 0; i < _puzzleValueIndicators.Length; i++)
            {
                UpdateLed(_puzzleValueIndicators[i], _puzzleValue[i]);
            }

            SetResolved(_puzzleValue.SequenceEqual(_targetPuzzleValue));
        }

        private void UpdateLed(LedLight light, int value)
        {
            switch (value)
            {
                case 1:
                    light.SetYellow();
                    break;
                case 2:
                    light.SetGreen();
                    break;
                case 3:
                    light.SetBlue();
                    break;
                case 4:
                    light.SetPink();
                    break;
                case 5:
                    light.SetRed();
                    break;
                default:
                    light.SetOff();
                    break;
            }

        }
    }

}