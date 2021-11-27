using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Puzzles.Base;
using UnityEngine;


namespace Gameplay.Puzzles.MarkerHitter
{
    public class MarkerHitterButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Camera _customRaycastCamera;
        [SerializeField] private AudioClip _successBeep;
        [SerializeField] private AudioClip _errorBeep;

        [SerializeField] private Material _offMaterial;
        [SerializeField] private Material _onMaterial;
        private bool locked;

        private AudioSource _audioSource;
        private Camera _raycastCamera;

        public event Action<MarkerHitterButton> OnButtonPressed;


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

        void Update()
        {
            if (!locked)
            {
                HandleMouseDown();
            }
        }

        private void HandleMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _raycastCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        OnButtonPressed?.Invoke(this);
                        SetMaterial(_onMaterial);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetMaterial(_offMaterial);
            }
        }

        public void Lock()
        {
            locked = true;
        }

        public void Unlock()
        {
            locked = false;
        }

        public void PlaySuccess()
        {
            _audioSource.PlayOneShot(_successBeep);
        }

        public void PlkayError()
        {
            _audioSource.PlayOneShot(_errorBeep);
        }

        private void SetMaterial(Material material)
        {
            Material[] mats = _renderer.materials;
            mats[1] = material;

            _renderer.materials = mats;
        }
    }
}