using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.Managers
{
    public class SceneLoader : MonoBehaviour
    {
#pragma warning disable 649
        public const string LevelPrefix = "Level";
        
        private string _currentLevelLoaded;
#pragma warning restore 649
        
        public void LoadLevelByFullName(string levelName, Action<AsyncOperation> levelLoadedCallback = null)
        {
            LoadLevel(levelName, levelLoadedCallback);
        }
        
        public void LoadLevelByLevelNumber(int levelNumber, Action<AsyncOperation> levelLoadedCallback = null)
        {
            LoadLevel(LevelPrefix + levelNumber, levelLoadedCallback);
        }

        public void UnloadCurrentLevel()
        {
            var operation = SceneManager.UnloadSceneAsync(_currentLevelLoaded);
            operation.completed += SceneUnloaded;
        }

        private void SceneUnloaded(AsyncOperation operation)
        {
            EventBroker.TriggerOnSceneUnloaded();
        }
        
        private void LoadLevel(string levelName, Action<AsyncOperation> levelLoadedCallback = null)
        {
            _currentLevelLoaded = levelName;
            
            var operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            operation.completed += levelLoadedCallback;
        }
    }
}
