using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Puzzles.Base;
using UnityEngine;


namespace Gameplay.Puzzles.LeverSwitches
{
    public class LeverSwitch : MonoBehaviour, IInteractable
    {
        public event Action<LeverSwitch> OnLeverSwitchToggled;

        private Camera _raycastCamera;

        [SerializeField] private Camera _customRaycastCamera;

        [SerializeField] private GameObject _lever;

        public float onValue { get; private set; }
        public float offValue { get; private set; }
        private bool _isLeverOn = false;
        private float _targetRotation = -45;

        private bool locked = false;

        void Awake()
        {
            onValue = UnityEngine.Random.Range(-0.2f, 0.2f);
            offValue = UnityEngine.Random.Range(-0.2f, 0.2f);

            if (!_customRaycastCamera)
            {
                _raycastCamera = Camera.main;
            }
            else
            {
                _raycastCamera = _customRaycastCamera;
            }

        }

        private void Update()
        {
            if (!locked)
            {
                HandleMouseClick();
            }
        }

        private void HandleMouseClick()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _raycastCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        _isLeverOn = !_isLeverOn;
                        _targetRotation = _isLeverOn ? 45 : -45;

                        OnLeverSwitchToggled.Invoke(this);
                    }
                }
            }

            // TOOD: Improve with async functions
            _lever.transform.localRotation = Quaternion.Lerp(_lever.transform.localRotation, Quaternion.Euler(_targetRotation, 0, 0), Time.deltaTime * 10);
        }

        public float PipeValueCalculation(float input)
        {
            return input + (_isLeverOn ? onValue : offValue);
        }

        public void Lock()
        {
            locked = true;
        }

        public void Unlock()
        {
            locked = false;
        }
    }

}