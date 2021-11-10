using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Puzzles.MarkerHitter
{
    public class MarkerHitterIndicator : MonoBehaviour
    {

        [SerializeField] private Transform _errorLight;
        [SerializeField] private Transform _successLight;

        public bool IsError { get; private set; }

        public void SetValid()
        {
            IsError = false;
            UpdateState();
        }

        public void SetInvalid()
        {
            IsError = true;
            UpdateState();
        }

        private void UpdateState() 
        {
            _errorLight.gameObject.SetActive(IsError);
            _successLight.gameObject.SetActive(!IsError);
        }
    }
}