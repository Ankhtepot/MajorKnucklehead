using System;
using System.Collections;
using System.Collections.Generic;
using Enumerations;
using Interface;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.Managers
{
    public class GameManager : MonoBehaviour
    {
#pragma warning disable 649
        [Header("Managers")]
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private GameSessionManager gameSessionManager;
        [SerializeField] private UIManager uiManager;
        [Header("Configurations")]
        [SerializeField] private List<ConfigurationBase> GameIniConfigurations = new List<ConfigurationBase>();
        [Header("Game Loop Setup")]
        [SerializeField] private float gameSessionToMenuDelay = 2f;
    
        public static GameState CurrentGameState => _currentGameState;
        public static Camera MainCamera => _cameraManager.MainCamera;
        public static AmmoManager AmmoManager => _ammoManager;
        public GameSessionManager GameSessionManager => gameSessionManager;
        
        public static PositionPointsManager PositionManager
        {
            get
            {
                if (!positionManager)
                {
                    positionManager = FindObjectOfType<PositionPointsManager>();
                }

                return positionManager;
            }
        }

        public SceneLoader SceneLoader => sceneLoader;
        public static ObjectPool.ObjectPool Pool => _pool;
        private static CameraManager _cameraManager;
        private static AmmoManager _ammoManager;
        private static PositionPointsManager positionManager;
        private static ObjectPool.ObjectPool _pool;
        private static GameState _currentGameState;
        private GameState _previousGameState;
#pragma warning restore 649
    
        private void Awake()
        {
            initialize();
        }
    
        public void ChangeGameState(GameState newGameState)
        {
            _previousGameState = _currentGameState;
            _currentGameState = newGameState;
        
            switch (newGameState)
            {
                case GameState.PreGameSession:
                    Time.timeScale = 0f;
                    break;
                case GameState.Running:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
            }

            if (_previousGameState != _currentGameState)
            {
                EventBroker.TriggerOnGameStateChanged(_previousGameState, _currentGameState);
            }
        }
        
        private void StopGameSession()
        {
            StartCoroutine(PostSessionRoutine(true));
        }

        private void ProcessWinCondition()
        {
            print("[Game Manager] All enemies killed, game session should end.");
            StartCoroutine(PostSessionRoutine());
        }

        private IEnumerator PostSessionRoutine(bool endImmediately = false)
        {
            yield return new WaitForSeconds(endImmediately ? 0f : gameSessionToMenuDelay);
            sceneLoader.UnloadCurrentLevel();
            ChangeGameState(GameState.PreGameSession);
            EventBroker.TriggerOnGameSessionStopped();
        }

        private void OnSceneLoaded()
        {
            positionManager = FindObjectOfType<PositionPointsManager>();
        }

        private void OnDisable()
        {
            EventBroker.OnGameSessionStartRequested -= gameSessionManager.StartGameSession;
            EventBroker.OnGameSessionStopRequested -= StopGameSession;
            EventBroker.OnWinConditionMet -= ProcessWinCondition;
            EventBroker.OnSceneLoaded -= OnSceneLoaded;
        }

        private void initialize()
        {
            GameIniConfigurations.ForEach(configuration => configuration.ActivateConfiguration());
            _cameraManager = GetComponent<CameraManager>();
            _ammoManager = GetComponent<AmmoManager>();
            _pool = GetComponentInChildren<ObjectPool.ObjectPool>();
            uiManager.gameSessionManager = gameSessionManager;
        
            EventBroker.OnGameSessionStartRequested += gameSessionManager.StartGameSession;
            EventBroker.OnGameSessionStopRequested += StopGameSession;
            EventBroker.OnWinConditionMet += ProcessWinCondition;
            EventBroker.OnSceneLoaded += OnSceneLoaded;
        
            ChangeGameState(GameState.PreGameSession);
        }
    }
}
