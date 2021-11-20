using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Puzzles.Base;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] private ValidStateLight _doorLight;
    [SerializeField] private Collider _exitCollider;
    [SerializeField] private AudioClip _doorOpenSound;

    private List<BasePuzzle> puzzles = new List<BasePuzzle>();
    private AudioSource _audioSource;

    [SerializeField] private ParticleSystem _smokeEmitter1;
    [SerializeField] private ParticleSystem _smokeEmitter2;

    private void Awake() {
        _exitCollider.enabled = false;

        var puzzleTags = GameObject.FindGameObjectsWithTag("Puzzle");

        foreach (var puzzle in puzzleTags) {
            var puzzleComponent = puzzle.GetComponent<BasePuzzle>();

            puzzleComponent.OnPuzzleResolved += PuzzleResolved;

            puzzles.Add(puzzleComponent);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void PuzzleResolved(object sender, BasePuzzle e)
    {
        foreach (var puzzle in puzzles) {
            if (!puzzle.IsResolved) {
                return;
            }
        }

        UnlockExit();
    }

    [ContextMenu("UnlockExit")]
    private async void UnlockExit() {
        _doorLight.SetValid(true);
        _exitCollider.enabled = true;
        _audioSource.PlayOneShot(_doorOpenSound);

        _smokeEmitter1.Play();
        _smokeEmitter2.Play();

        await Task.Delay(1500);

        _smokeEmitter1.Stop();
        _smokeEmitter2.Stop();
    }
}
