using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;

namespace Argis.MenuSystem.Runtime
{
    /// <summary>
    /// Class for managing the Menus
    /// </summary>
    /// <see cref="https://github.com/UnityGameAcademy/LevelManagementUnity/blob/master/Assets/LevelManagement/Scripts/MenuManager.cs"/>
    public sealed class MenuManager : MonoBehaviour
    {
        // TODO:// Refactor to use AssetReferences instead of GameObjects for better memory management
        [SerializeField]
        [Tooltip("Add all menus to be used at runtime.")]
        private Menu[] _menus;

        [SerializeField]
        [Tooltip("Transform for organizing your Menus, defaults to Menus object")]
        private Transform _menuParent;

        // stack for tracking our active Menus
        private Stack<Menu> _menuStack = new Stack<Menu>();

        // single instance
        private static MenuManager _instance;
        public static MenuManager Instance { get { return _instance; } }

        [SerializeField]
        private bool _openMainMenuAtStartUp = true;


        private void Awake()
        {
            // self-destruct if a MenuManager instance already exists
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                // otherwise, assign this object as the Singleton instance
                _instance = this;

                // initialize Menus and make this GameObject persistent
                InitializeMenus();
                DontDestroyOnLoad(gameObject);
            }
        }

        // remove Singleton instance if this object is destroyed
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        // create the Menus at the start of the game
        private void InitializeMenus()
        {

            // generate a default parent object if none specified
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            // mark the parent object as persistent
            DontDestroyOnLoad(_menuParent.gameObject);

            foreach (var menu in _menus)
            {
                // ... instantiate it
                Menu menuInstance = Instantiate(menu, _menuParent);
                // disable the Menu object unless it is the MainMenu
                if (_openMainMenuAtStartUp && menu is MainMenu)
                {
                    OpenMenu(menuInstance);
                }
                else
                {
                    menuInstance.gameObject.SetActive(false);
                }
            }
        }

        // open a Menu and add to the Menu stack
        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogError("MENUMANAGER OpenMenu ERROR: invalid menu");
                return;
            }

            var topCanvas = menuInstance.GetComponent<Canvas>();
            // disable all of the previous menus in the stack
            if (_menuStack.Count > 0)
            {
                if (menuInstance.DisableMenusUnderneath)
                {
                    foreach (Menu menu in _menuStack)
                    {
                        menu.gameObject.SetActive(false);
                        if (menu.DisableMenusUnderneath)
                            break;
                    }
                }

                var previousCanvas = _menuStack.Peek().GetComponent<Canvas>();
                topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;
            }
            else
            {
                topCanvas.sortingOrder = 0;
            }

            // activate the Menu and add to the top of the Menu stack
            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);
        }

        // close a Menu and remove it from the Menu stack
        public void CloseMenu()
        {
            // if the stack is empty, do nothing
            if (_menuStack.Count == 0)
            {
                Debug.LogWarning("MENUMANAGER CloseMenu ERROR: No menus in stack!");
                return;
            }

            CloseTopMenu();
        }

        public void CloseTopMenu()
        {
            var instance = _menuStack.Pop();

            if (instance.DestroyWhenClosed)
                Destroy(instance.gameObject);
            else
                instance.gameObject.SetActive(false);

            // Re-activate top menu
            // If a re-activated menu is an overlay we need to activate the menu under it
            foreach (var menu in _menuStack)
            {
                menu.gameObject.SetActive(true);

                if (menu.DisableMenusUnderneath)
                    break;
            }
        }

        public void CreateInstance<T>() where T : Menu
        {
            var prefab = GetPrefab<T>();

            Instantiate(prefab, _menuParent);
        }

        private T GetPrefab<T>() where T : Menu
        {
            //// Get prefab dynamically, based on public fields set from Unity
            foreach (var menu in _menus)
            {
                if (menu is T)
                {
                    return (T)menu;
                }
            }

            throw new MissingReferenceException("Prefab not found for type " + typeof(T));
        }

        // TODO:// Refactor to use new input system
        //#if ANDROID || UNITY_EDITOR
        //        // Does not use new input system
        //        private void Update()
        //        {
        //            // On Android the back button is sent as Esc
        //            if (Input.GetKeyDown(KeyCode.Escape) && _menuStack.Count > 0)
        //            {
        //                _menuStack.Peek().OnBackPressed();
        //            }
        //        }
        //#endif
    }
}