using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Puzzles.Base;
using UnityEngine;

public class LedLight : MonoBehaviour
{
    [SerializeField] private Material _offMaterial;
    [SerializeField] private Material _yellowMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;

    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _pinkMaterial;
    [SerializeField] private Renderer _object;

    public void SetOff()
    {
        SetMaterial(_offMaterial);
    }

    public void SetYellow()
    {
        SetMaterial(_yellowMaterial);
    }

    public void SetRed()
    {
        SetMaterial(_redMaterial);
    }

    public void SetGreen()
    {
        SetMaterial(_greenMaterial);
    }

    public void SetPink()
    {
        SetMaterial(_pinkMaterial);
    }

    public void SetBlue()
    {
        SetMaterial(_blueMaterial);
    }


    private void SetMaterial(Material material)
    {
        Material[] mats = _object.materials;
        mats[1] = material;

        _object.materials = mats;
    }
}