using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Puzzles.Base;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] private ValidStateLight _doorLight;
    [SerializeField] private Collider _exitCollider;

    private List<BasePuzzle> puzzles = new List<BasePuzzle>();

    private void Awake() {
        _exitCollider.enabled = false;

        var puzzleTags = GameObject.FindGameObjectsWithTag("Puzzle");

        foreach (var puzzle in puzzleTags) {
            var puzzleComponent = puzzle.GetComponent<BasePuzzle>();

            puzzleComponent.OnPuzzleResolved += PuzzleResolved;

            puzzles.Add(puzzleComponent);
        }
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

    private void UnlockExit() {
        _doorLight.SetValid(true);
        _exitCollider.enabled = true;
    }
}
