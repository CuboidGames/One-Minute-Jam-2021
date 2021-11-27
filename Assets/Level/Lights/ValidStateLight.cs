using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidStateLight : MonoBehaviour
{
    [SerializeField] private GameObject _validLight;
    [SerializeField] private GameObject _invalidLight;

    private bool _valid = false;

    public void SetValid(bool valid)
    {
        _valid = valid;

        _validLight.SetActive(_valid);
        _invalidLight.SetActive(!_valid);
    }
}
