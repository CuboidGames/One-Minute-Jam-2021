using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Puzzles.Base
{
    public abstract class BasePuzzle : MonoBehaviour
    {

        [SerializeField] private GameObject[] _interactables;

        protected List<IInteractable> actualInteractables = new List<IInteractable>();

        [SerializeField] protected ValidStateLight stateLight;

        public bool IsResolved { get; private set; }

        protected bool IsLocked = false;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip _puzzleResolvedSound;

        public event EventHandler<BasePuzzle> OnPuzzleResolved;

        protected void Awake()
        {
            foreach (var interactable in _interactables)
            {
                if (interactable.TryGetComponent<IInteractable>(out IInteractable actualInteractable))
                {
                    actualInteractables.Add(actualInteractable);
                }
            }

            _audioSource = GetComponent<AudioSource>();
        }

        public void Lock()
        {
            foreach (var interactable in actualInteractables)
            {
                interactable.Lock();
            }

            IsLocked = true;
        }

        public void Unlock()
        {
            foreach (var interactable in actualInteractables)
            {
                interactable.Unlock();
            }

            IsLocked = false;
        }

        protected void SetResolved(bool resolved)
        {
            if (resolved)
            {
                IsResolved = true;
                stateLight.SetValid(true);
                _audioSource.PlayOneShot(_puzzleResolvedSound);

                Lock();

                if (OnPuzzleResolved != null)
                {
                    OnPuzzleResolved.Invoke(this, this);
                }
            }
            else
            {
                IsResolved = false;
                Unlock();

                stateLight.SetValid(false);
            }
        }
    }
}