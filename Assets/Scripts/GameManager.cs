using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enumerations;
using Interface;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class GameManager : MonoBehaviour
{
#pragma warning disable 649
    
#pragma warning restore 649

    [SerializeField] private List<ConfigurationBase> GameIniConfigurations = new List<ConfigurationBase>();
    public UnityAction<GameState, GameState> OnGameStateChanged;
    public static GameState CurrentGameState => _currentGameState;

    private static GameState _currentGameState;
    private GameState _previousGameState;
    
    private void Start()
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
        var test = DOTween.defaultAutoKill;
        EventBroker.OnGameSessionStartRequested += StartGameSession;
        EventBroker.OnGameSessionStopRequested += StopGameSession;
        ChangeGameState(GameState.PreGameSession);
    }
}
