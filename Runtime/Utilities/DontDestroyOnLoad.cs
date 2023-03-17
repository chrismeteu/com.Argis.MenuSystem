using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Argis.MenuSystem.Runtime.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        // makes an object persistent in between scene loads 
        private void Awake()
        {
            transform.SetParent(null);
            Object.DontDestroyOnLoad(gameObject);
        }
    }
}