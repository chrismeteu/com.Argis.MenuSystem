using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Argis.MenuSystem.Runtime
{
    // a specialized ScreenFader to launch the application
    [RequireComponent(typeof(ScreenFader))]
    public class SplashScreen : MonoBehaviour
    {
        // reference to the ScreenFader component
        protected ScreenFader _screenFader;

        // delay in seconds
        [SerializeField]
        protected float delay = 1f;

        // assign the ScreenFader component
        protected virtual void Awake()
        {
            _screenFader = GetComponent<ScreenFader>();
        }

        // fade on the ScreenFader when we start
        protected virtual void Start()
        {
            _screenFader.FadeOn();
            FadeAndLoad();
        }

        // fade off the ScreenFader and load the MainMenu
        protected virtual void FadeAndLoad()
        {
            StartCoroutine(FadeAndLoadRoutine());
        }

        // coroutine to fade off the ScreenFader and load the MainMenu
        protected virtual IEnumerator FadeAndLoadRoutine()
        {
            // wait for a delay
            yield return new WaitForSeconds(delay + _screenFader.FadeOnDuration);

            // load the Main scene
            LevelLoader.LoadMainScene();

            // fade off
            _screenFader.FadeOff();

            // wait for fade to complete
            yield return new WaitForSeconds(_screenFader.FadeOffDuration);

            // destroy the SplashScreen object
            Object.Destroy(gameObject);
        }
    }
}