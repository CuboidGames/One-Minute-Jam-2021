using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Managers;
using Gameplay.Managers.GameStateManager;
using Gameplay.Puzzles.Base;
using UnityEngine;

public class ExitCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.exitColliderTouched = true;
        }
    }
}
