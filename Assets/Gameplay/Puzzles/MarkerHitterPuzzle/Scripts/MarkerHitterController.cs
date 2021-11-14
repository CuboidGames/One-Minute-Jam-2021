using System.Collections;
using System.Collections.Generic;
using Gameplay.Puzzles.Base;
using UnityEngine;

namespace Gameplay.Puzzles.MarkerHitter
{
    public class MarkerHitterController : BasePuzzle
    {
        [SerializeField] private Collider _marker;
        [SerializeField] private Collider _targetArea;
        [SerializeField] private MarkerHitterButton _hitterButton;


        [SerializeField] private float _timeMultiplier = 4.5f;
        [SerializeField] private float _amplitude = 0.15f;
        [SerializeField] private MarkerHitterIndicator[] _markerIndicators;
        private int _currentValidIndex = -1;

        private new void Awake()
        {
            base.Awake();

            _hitterButton.OnButtonPressed += OnButtonPressed;
        }

        private void Update()
        {
            if (!IsLocked) {
                SetMarkerPosition();
            }
        }

        private void SetMarkerPosition()
        {
            _marker.transform.localPosition = new Vector3(Mathf.Sin(Time.time * _timeMultiplier) * _amplitude, _marker.transform.localPosition.y, _marker.transform.localPosition.z);
        }

        private void OnButtonPressed()
        {
            if (_marker.bounds.Intersects(_targetArea.bounds))
            {
                _currentValidIndex++;

                _markerIndicators[_currentValidIndex].SetValid();
            }
            else
            {
                foreach (var indicator in _markerIndicators)
                {
                    indicator.SetInvalid();
                }

                _currentValidIndex = -1;
            }

            SetResolved(_currentValidIndex == _markerIndicators.Length - 1);
        }
    }
}