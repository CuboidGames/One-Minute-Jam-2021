using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Puzzles.Base;
using UnityEngine;


namespace Gameplay.Puzzles.MarkerHitter
{
    public class MarkerHitterButton : MonoBehaviour, IInteractable
    {
        public event Action OnButtonPressed;

        private Camera _raycastCamera;

        [SerializeField] private Camera _customRaycastCamera;
        private bool locked;

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
                        OnButtonPressed.Invoke();
                    }
                }
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
    }
}