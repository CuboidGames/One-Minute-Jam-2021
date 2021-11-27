using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Puzzles.Base;
using Gameplay.Puzzles.Pairs;
using UnityEngine;

namespace Gameplay.Puzzles.MarkerHitter
{
    public class MarkerHitterController : BasePuzzle
    {
        [SerializeField] private MarkerHitterButton _hitterButton;
        [SerializeField] private LedLight[] _markerLeds;
        [SerializeField] private GameObject _pressLabel;
        [SerializeField] private Material _offMaterial;
        [SerializeField] private Material _onMaterial;

        [SerializeField] private Renderer _renderer;
        private bool _lockClick = false;
        private int _currentValidIndex = -1;

        public bool canPress { get; private set; }

        private new void Awake()
        {
            base.Awake();

            _hitterButton.OnButtonPressed += OnButtonPressed;
        }

        private void Update()
        {
            if (!IsLocked)
            {
                SetPressState();
                SetMarkerVisibility();
            }
        }

        private void SetPressState()
        {
            canPress = (Time.time % 1) < 0.2f;
        }

        private void SetMarkerVisibility()
        {
            _pressLabel.SetActive(canPress);
            SetMaterial(canPress ? _onMaterial : _offMaterial);
        }

        private void OnButtonPressed(MarkerHitterButton button)
        {
            if (_lockClick)
            {
                return;
            }

            if (canPress)
            {
                _currentValidIndex++;
                _markerLeds[_currentValidIndex].SetGreen();
                button.PlaySuccess();
            }
            else
            {
                _currentValidIndex++;
                _markerLeds[_currentValidIndex].SetRed();
                button.PlkayError();

                SleepAndReset();

                _currentValidIndex = -1;
            }

            SetResolved(_currentValidIndex == _markerLeds.Length - 1);
        }

        private async void SleepAndReset()
        {
            _lockClick = true;

            await Task.Delay(300);

            foreach (var led in _markerLeds)
            {
                led.SetOff();
            }

            _lockClick = false;
        }


        private void SetMaterial(Material material)
        {
            _renderer.material = material;
        }

        public override void Init()
        {
            foreach (var led in _markerLeds)
            {
                led.SetOff();
            }

            _currentValidIndex = -1;
            _lockClick = false;

            SetResolved(false);
        }
    }
}