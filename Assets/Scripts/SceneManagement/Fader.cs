using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 1f;

        private Coroutine currentActiveFader = null;

        // Start is called before the first frame update
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeOutIn());
        }

        public void FadeOutImmediately()
        {
            canvasGroup.alpha = 1;
        }

       public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }


        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFader != null)
            {
                StopCoroutine(currentActiveFader);
            }

            currentActiveFader = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFader;
        }

         IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }

        IEnumerator FadeInRoutine(float time)
        {

            while (canvasGroup.alpha > 0)
            {
                // moving toward alpha 1;
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(fadeOutTime);
            print("fade out");
            yield return FadeIn(fadeInTime);
            print("fade in");
        }


    }
}