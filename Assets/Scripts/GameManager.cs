﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enumerations;
using Interface;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Utilities.Managers;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

public class GameManager : MonoBehaviour
{
#pragma warning disable 649
    
#pragma warning restore 649
    [Header("Configurations")]
    [SerializeField] private List<ConfigurationBase> GameIniConfigurations = new List<ConfigurationBase>();
    
    public UnityAction<GameState, GameState> OnGameStateChanged;
    public static GameState CurrentGameState => _currentGameState;
    public static Camera MainCamera => _cameraManager.MainCamera;
    public static AmmoManager AmmoManager => _ammoManager;

    public static ObjectPool Pool => _pool;

    private static CameraManager _cameraManager;
    private static AmmoManager _ammoManager;
    private static ObjectPool _pool;
    private static GameState _currentGameState;
    private GameState _previousGameState;
    
    private void Awake()
    {
        initialize();
    }

    private void StartGameSession()
    {
        ChangeGameState(GameState.Running);
        EventBroker.TriggerOnGameSessionStarted();
    }

    private void StopGameSession()
    {
        ChangeGameState(GameState.PreGameSession);
        EventBroker.TriggerOnGameSessionStopped();
    }
    
    private void ChangeGameState(GameState newGameState)
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
    
    private void initialize()
    {
        GameIniConfigurations.ForEach(configuration => configuration.ActivateConfiguration());
        _cameraManager = GetComponent<CameraManager>();
        _ammoManager = GetComponent<AmmoManager>();
        _pool = GetComponentInChildren<ObjectPool>();
        
        EventBroker.OnGameSessionStartRequested += StartGameSession;
        EventBroker.OnGameSessionStopRequested += StopGameSession;
        
        ChangeGameState(GameState.PreGameSession);
    }
}
