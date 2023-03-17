using UnityEngine;

namespace Argis.MenuSystem.Runtime.Utilities
{
    /// <summary>
    /// Removes a Gameobject not meant to be included at runtime but used only
    /// while in the Unity Editor.
    ///
    /// The exucition order must be early enough to make sure no outside code
    /// can communicate or use the GameObject and anything on it.
    /// </summary>
    [DefaultExecutionOrder(-10000000)]
    public class RemoveAtRuntime : MonoBehaviour
    {
        private void Awake()
        {
            DestroyImmediate(gameObject);
        }
    }
}