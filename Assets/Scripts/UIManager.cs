using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Extensions;

//Fireball Games * * * PetrZavodny.com

public class UIManager : MonoBehaviour
{
#pragma warning disable 649
    [Header("Start Game Button")] 
    [SerializeField] private Button startGameButton;

    [SerializeField] private float startGameButtonAnimationDuration = 1f;

    private Sequence startButtonSequence; 
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    private void OnGameStarted()
    {
        
    }

    private Sequence SetStartButtonSequence()
    {
        var sequence = DOTween.Sequence().Pause();
        sequence.Append(startGameButton.transform.DOLocalMoveX(560, startGameButtonAnimationDuration));
        var textPart = startGameButton.GetComponentInChildren<TextMeshProUGUI>();
        sequence.Insert(0, textPart.DOColor(textPart.color.ZeroAlpha(), startGameButtonAnimationDuration));
        
        return null;
    }

    private void OnDisable()
    {
        EventBroker.OnGameSessionStarted -= OnGameStarted;
    }

    private void initialize()
    {
        EventBroker.OnGameSessionStarted += OnGameStarted;
        DOTween.defaultEaseType = Ease.InOutCubic;
        DOTween.defaultAutoKill = false;
        SetStartButtonSequence();
    }
}
