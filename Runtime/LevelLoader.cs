using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Argis.MenuSystem.Runtime
{
    public class LevelLoader : MonoBehaviour
    {
        // index of the Main scene level
        private static int mainSceneIndex = 1;

        // loads a level by name
        public static void LoadLevel(string levelName)
        {
            // if the scene is in the BuildSettings, load the scene
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning("GAMEMANAGER LoadLevel Error: invalid scene specified!");
            }
        }

        // loads a level by index
        public static void LoadLevel(int levelIndex)
        {
            // if the index is valid...
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                //// open the MainMenu if the index is the mainMenuIndex
                //if (levelIndex == LevelLoader.mainSceneIndex)
                //{
                //    MainMenu.Open();
                //}

                // load the scene by index
                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER LoadLevel Error: invalid scene specified!");
            }
        }

        // reloads the currently active scene
        public static void ReloadLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().name);
        }

        // loads the next scene in the BuildSettings, wraps back to MainMenu if we run out of scenes
        public static void LoadNextLevel()
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1)
                % SceneManager.sceneCountInBuildSettings;
            nextSceneIndex = Mathf.Clamp(nextSceneIndex, mainSceneIndex, nextSceneIndex);
            LoadLevel(nextSceneIndex);

        }

        // loads the MainMenu level
        public static void LoadMainScene()
        {
            LoadLevel(mainSceneIndex);
        }

    }
}