using UnityEngine;

namespace Argis.MenuSystem.Runtime
{
    [CreateAssetMenu(fileName = "MenuList.asset", menuName = "Argis/Menus/Menu List")]
    public class MenuList : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Add all menus to be used at runtime.")]
        private Menu[] _menus;
        public Menu[] Menus => _menus;
    }
}