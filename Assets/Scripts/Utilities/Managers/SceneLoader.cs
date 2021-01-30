using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.Managers
{
    public class SceneLoader : MonoBehaviour
    {
#pragma warning disable 649
        private int _currentLevelLoaded = 0;
#pragma warning restore 649
        
        public void LoadLevel(string levelName, Action<AsyncOperation> levelLoadedCallback = null)
        {
            var operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            operation.completed += levelLoadedCallback;
        }
    }
}
