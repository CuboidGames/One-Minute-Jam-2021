using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldRotator : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0.1f * speed * Time.deltaTime, 0));
    }
}
