using System.Threading.Tasks;
using Gameplay.Managers;
using UnityEngine;

namespace Gameplay.Player
{
    public class WalkerAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _step1;
        [SerializeField] private AudioClip _step2;

        [SerializeField] private AudioClip _exitSceneClip;

        private AudioSource _audioSource;

        private bool running = true;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            SceneTransitionManager.Instance.FadeOut();
        }

        public void ReproduceStep()
        {
            if (!running)
            {
                return;
            }

            _audioSource.clip = Random.value > 0.5f ? _step1 : _step2;
            _audioSource.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ExitScene"))
            {
                StartExitScene();
            }
        }

        private async void StartExitScene()
        {
            running = false;
            await SceneTransitionManager.Instance.FadeIn(2f);
            _audioSource.PlayOneShot(_exitSceneClip);

            await Task.Delay(1000);

            SceneTransitionManager.Instance.LoadScene("MainScene");
        }

    }
}