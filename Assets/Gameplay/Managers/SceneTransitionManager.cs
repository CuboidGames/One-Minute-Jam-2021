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
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public async Task FadeIn()
        {
            await Fade(0f, 1f);
        }

        public async Task FadeOut()
        {
            await Fade(1f, 0f);
        }

        private async Task Fade(float from, float to) {
            float currentAlpha = from;

            while (Mathf.Abs(currentAlpha - to) != 0) {
                _fader.color = new Color(0, 0, 0, currentAlpha);
                currentAlpha = Mathf.MoveTowards(currentAlpha, to, 0.3f * Time.deltaTime);

                await Task.Yield();
            }
        }
    }
}