using UnityEngine;

namespace Argis.MenuSystem.Runtime.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        /// <summary>
        /// Makes an object persistent in between scene loads 
        /// </summary>
        private void Awake()
        {
            transform.SetParent(null);
            Object.DontDestroyOnLoad(gameObject);
        }
    }
}