using UnityEditor;
using UnityEngine;

namespace Argis.MenuSystem.Runtime
{
    /// <summary>
    /// Generic class for Menus, implementing Singleton pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        /// <summary>
        /// Reference to public and private instances
        /// </summary>
        private static T _instance;
        public static T Instance { get { return _instance; } }

        /// <summary>
        /// Self-destruct if another instance already exists
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
            }
        }

        /// <summary>
        /// Unset the instance if this object is destroyed
        /// </summary>
        protected virtual void OnDestroy()
        {
            _instance = null;
        }

        /// <summary>
        /// Simplifies syntax to open a menu
        /// </summary>
        public static void Open()
        {
            if (MenuManager.Instance != null && _instance != null)
            {
                MenuManager.Instance.OpenMenu(_instance);
            }
            else if (MenuManager.Instance != null)
            {
                // Recreate menu and open
                MenuManager.Instance.CreateInstance<T>();
                if (_instance != null)
                    MenuManager.Instance.OpenMenu(_instance);
                else
                    Debug.LogWarning($"Could not find and open menu");
            }
            else
            {
                Debug.LogWarning($"Could not open menu");
            }
        }

    }

    /// <summary>
    /// Base class for all Menus
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        [Header("Characteristics")]

        /// <summary>
        /// Destroy the Game Object when menu is closed (reduces memory usage)
        /// </summary>
        [Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")]
        public bool DestroyWhenClosed = true;

        [Tooltip("Disable menus that are under this one in the stack")]
        public bool DisableMenusUnderneath = true;

        [Header("Audiio")]

        [SerializeField]
        [Tooltip("Audio source (one can be found on the template prefab)")]
        protected AudioSource source;
        [SerializeField]
        [Tooltip("")]
        protected AudioClip buttonTapClip;

        [SerializeField]
        protected bool pauseGameWhenOpened = false;

        protected virtual void OnEnable()
        {
            if (pauseGameWhenOpened)
            {
                Debug.Log("Game play has been paused.");
                Time.timeScale = 0;
            }
        }

        /// <summary>
        /// Closes the menu
        /// </summary>
        public virtual void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }

            if (pauseGameWhenOpened)
            {
                Debug.Log("Game play has been unpaused.");
                Time.timeScale = 1;
            }
        }

        public virtual void OnOKPressed() { Debug.LogWarning("Button method has not been implemented"); }
        public virtual void OnCancelPressed() { Debug.LogWarning("Button method has not been implemented"); }
        public virtual void OnResetPressed() { Debug.LogWarning("Button method has not been implemented"); }
        public virtual void OnSubmitPressed() { Debug.LogWarning("Button method has not been implemented"); }
    }
}