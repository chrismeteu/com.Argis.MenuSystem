using System.Collections;
using System.Collections.Generic;
using Argis.MenuSystem.Runtime;
using UnityEngine;


namespace Argis.MenuSystem.Runtime
{
    /// <summary>
    /// Specialized ScreenFader to hide the transition between scenes
    /// </summary>
    public class TransitionFader : ScreenFader
    {
        // duration of the transition
        [SerializeField]
        private float _lifetime = 1f;

        // delay before start fading
        [SerializeField]
        private float _delay = 0.3f;
        public float Delay { get { return _delay; } }

        // calculate the minimum lifetime
        protected void Awake()
        {
            _lifetime = Mathf.Clamp(_lifetime, FadeOnDuration + FadeOffDuration + _delay, 10f);
        }

        // coroutine to fade on, wait, and fade off
        private IEnumerator PlayRoutine()
        {
            SetAlpha(_clearAlpha);
            yield return new WaitForSeconds(_delay);

            FadeOn();
            float onTime = _lifetime - (FadeOffDuration + _delay);

            yield return new WaitForSeconds(onTime);

            FadeOff();
            Object.Destroy(gameObject, FadeOffDuration);
        }

        public void Play()
        {
            StartCoroutine(PlayRoutine());
        }

        // instantiate a transition prefab and fade on/off
        public static void PlayTransition(TransitionFader transitionPrefab)
        {
            if (transitionPrefab != null)
            {
                TransitionFader instance = Instantiate(transitionPrefab, Vector3.zero, Quaternion.identity);
                instance.Play();
            }
        }
    }
}
//using System.Collections;
//using Argis.MenuSystem.Runtime;
//using UnityEngine;

//namespace Argis.MenuSystem.Runtime
//{
//    public class TransitionFader : ScreenFader
//    {
//        [SerializeField, Tooltip("Duration of the transition")]
//        private float _lifetime = 1f;

//        [SerializeField, Tooltip("Delay before start fading")]
//        private float _delay = 0.3f;
//        /// <summary>
//        /// Delay before start fading
//        /// </summary>
//        public float Delay { get { return _delay; } }

//        protected void Awake()
//        {
//            // Calculate the minimum lifetime
//            _lifetime = Mathf.Clamp(_lifetime, FadeOnDuration + FadeOffDuration + _delay, 10f);
//        }

//        /// <summary>
//        /// Coroutine to fade on, wait, and fade off
//        /// </summary>
//        /// <returns>Can be yielded</returns>
//        private IEnumerator PlayRoutine()
//        {
//            SetAlpha(_clearAlpha);
//            yield return new WaitForSeconds(_delay);

//            FadeOn();
//            float onTime = _lifetime - (FadeOffDuration + _delay);

//            yield return new WaitForSeconds(onTime);

//            FadeOff();
//            Destroy(gameObject, FadeOffDuration);
//        }

//        /// <summary>
//        /// Play the transition
//        /// </summary>
//        public void Play()
//        {
//            StartCoroutine(PlayRoutine());
//        }

//        /// <summary>
//        /// Instantiate a transition prefab and fade on/off
//        /// </summary>
//        /// <param name="transitionPrefab"></param>
//        public static void PlayTransition(TransitionFader transitionPrefab)
//        {
//            if (transitionPrefab != null)
//            {
//                TransitionFader instance = Instantiate(transitionPrefab, Vector3.zero, Quaternion.identity);
//                instance.Play();
//            }
//        }
//    }
//}