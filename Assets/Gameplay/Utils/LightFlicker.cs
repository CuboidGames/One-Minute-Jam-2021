using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float min = 1;
    [SerializeField] private float max = 2;

    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        _light.intensity = Random.Range(min, max);
    }
}