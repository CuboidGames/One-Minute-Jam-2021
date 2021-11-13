using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public class MovementController : MonoBehaviour
    {

        [SerializeField] private float speed = 5.0f;

        private Rigidbody _rb;

        private float verticalMovement;
        private float horizontalMovement;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            verticalMovement = Input.GetAxis("Vertical");
            horizontalMovement = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + new Vector3(horizontalMovement * speed * Time.fixedDeltaTime, 0, verticalMovement * speed * Time.fixedDeltaTime));
        }

    }
}