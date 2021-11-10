using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Puzzles.MarkerHitter
{
    public class MarkerHitterButton : MonoBehaviour
    {
        public event Action OnButtonPressed;

        private Camera _raycastCamera;

        [SerializeField] private Camera _customRaycastCamera;

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
    }
}