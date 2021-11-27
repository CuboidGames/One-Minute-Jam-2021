using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Managers
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public static SceneTransitionManager Instance;

        [SerializeField] private Image _fader;
        void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        public async Task FadeIn(float speed) {
            await Fade(0f, 1f, speed);
        }

        public async Task FadeIn()
        {
            await Fade(0f, 1f, 0.3f);
        }

        public async Task FadeOut(float speed) {
            await Fade(1f, 0f, speed);
        }

        public async Task FadeOut()
        {
            await Fade(1f, 0f, 0.3f);
        }

        private async Task Fade(float from, float to, float speed) {
            float currentAlpha = from;

            while (Mathf.Abs(currentAlpha - to) != 0) {
                _fader.color = new Color(0, 0, 0, currentAlpha);
                currentAlpha = Mathf.MoveTowards(currentAlpha, to, speed * Time.deltaTime);

                await Task.Yield();
            }
        }

        public void LoadScene(string sceneName) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}