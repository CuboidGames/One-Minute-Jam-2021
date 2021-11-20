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

        [SerializeField] private Renderer _renderer;

        public bool[] onValue { get; private set; }
        private bool _isButtonOn = false;
        private bool locked = false;

        private AudioSource _audioSource;

        [SerializeField] private AudioClip _successBeep;
        [SerializeField] private AudioClip _errorBeep;

        void Awake()
        {
            if (!_customRaycastCamera)
            {
                _raycastCamera = Camera.main;
            }
            else
            {
                _raycastCamera = _customRaycastCamera;
            }

            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!locked)
            {
                HandleMouseClick();
            }
        }

        public void Init(int items) {
            _isButtonOn = false;
            SetMaterial(_offMaterial);

            onValue = new bool[items];

            for (var i = 0; i < items; i++) {
                onValue[i] = UnityEngine.Random.value > 0.33f;
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
                        _isButtonOn = !_isButtonOn;
                        
                        SetMaterial(_isButtonOn ? _onMaterial : _offMaterial);

                        OnLeverSwitchToggled.Invoke(this);

                        _audioSource.PlayOneShot(_successBeep);
                    }
                }
            }
        }

        public int[] PipeValueCalculationConditional(int[] input)
        {
            if (!_isButtonOn) {
                return input;
            }

            return PipeValueCalculation(input);
        }

        public int[] PipeValueCalculation(int[] input)
        {
            int[] output = new int[input.Length];
            
            for (var i = 0; i < input.Length; i++) {
                output[i] = input[i] + (onValue[i] ? 1 : 0);
            }

            return output;
        }

        public void Lock()
        {
            locked = true;
        }

        public void Unlock()
        {
            locked = false;
        }

        [SerializeField] private Material _offMaterial;
        [SerializeField] private Material _onMaterial;

        private void SetMaterial(Material material)
        {
            Material[] mats = _renderer.materials;
            mats[1] = material;

            _renderer.materials = mats;
        }
    }

}