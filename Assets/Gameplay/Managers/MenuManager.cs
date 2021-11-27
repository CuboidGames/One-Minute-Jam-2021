using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _buttonClickSound;
        private AudioSource _audioSource;

        private bool _starting = false;

        public void Awake()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            _audioSource = GetComponent<AudioSource>();
        }

        public void Start()
        {
            SceneTransitionManager.Instance.FadeOut();
        }
        public async void StartGame()
        {
            if (_starting)
            {
                return;
            }

            _starting = true;

            _audioSource.PlayOneShot(_buttonClickSound);

            await SceneTransitionManager.Instance.FadeIn();
            SceneTransitionManager.Instance.LoadScene("IntroScene");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}