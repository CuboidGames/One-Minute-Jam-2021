using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Player
{
    public class CursorController : MonoBehaviour
    {

        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;

        private Image _cursor;

        private Camera _raycastCamera;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (_camera)
            {
                _raycastCamera = _camera;
            }
            else
            {
                _raycastCamera = Camera.main;
            }

            _cursor = GetComponent<Image>();
        }

        private void Update()
        {
            Ray ray = _raycastCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            _cursor.enabled = Physics.Raycast(ray, out hit, 2f, _layerMask);
        }
    }
}