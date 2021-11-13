using UnityEngine;

namespace Gameplay.Player
{
    public class MouseCameraController : MonoBehaviour
    {

        [SerializeField] private float sensitivity = 1.5f;
        [SerializeField] private Rigidbody character;
        private Vector2 mouseLook;
        private float horizontalMovement;
        private float verticalMovement;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            horizontalMovement = Input.GetAxisRaw("Mouse X") * sensitivity;
            verticalMovement = Input.GetAxisRaw("Mouse Y") * sensitivity;

            mouseLook += new Vector2(horizontalMovement, verticalMovement) * Time.deltaTime;
            mouseLook = new Vector2(mouseLook.x, Mathf.Clamp(mouseLook.y, -90f, 90f));

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        }

        private void FixedUpdate()
        {
            character.MoveRotation(Quaternion.AngleAxis(horizontalMovement * Time.fixedDeltaTime, character.transform.up));
        }
    }
}