using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Puzzles.Base;
using UnityEngine;

namespace Gameplay.Puzzles.Pairs
{
    public enum PairColor
    {
        RED,
        BLUE,
        GREEN,
        YELLOW,
    }

    [Serializable]
    public struct MaterialColorTuple
    {
        public Material material;
        public PairColor color;
    }

    public class PairButton : MonoBehaviour, IInteractable
    {

        public bool isRevealed { get; private set; }
        private Camera _raycastCamera;

        [SerializeField] private Camera _customRaycastCamera;

        public event Action<PairButton> OnPairButtonRevealed;

        [SerializeField] public PairColor _color;

        public PairColor color { get { return _color; } }

        [SerializeField] private MaterialColorTuple[] _materials;

        [SerializeField] private MeshRenderer _buttonRenderer;

        [SerializeField] private Material _hiddenMaterial;
        [SerializeField] private Dictionary<PairColor, Material> _materialsMap;
        private bool locked;

        private void Awake()
        {
            _materialsMap = new Dictionary<PairColor, Material>();

            foreach (var material in _materials)
            {
                _materialsMap.Add(material.color, material.material);
            }

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
                        Reveal();
                    }
                }
            }
        }

        public void SetColor(PairColor color)
        {
            _color = color;
        }

        public void Conceal()
        {
            _buttonRenderer.material = _hiddenMaterial;
            isRevealed = false;
        }

        public void Reveal()
        {
            _buttonRenderer.material = _materialsMap[_color];
            isRevealed = true;

            if (OnPairButtonRevealed != null)
            {
                OnPairButtonRevealed(this);
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