using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Extensions;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.Managers
{
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

        public void OnStartGameSessionButtonClicked() => EventBroker.TriggerOnGameSessionStartRequested();

        public void OnStopGameSessionButtonClicked() => EventBroker.TriggerOnGameSessionStopRequested();

        private void OnGameSessionStarted() => startButtonSequence.Restart();
    
        private void OnGameSessionStopped()
        {
            startGameButton.gameObject.SetActive(true);
            startButtonSequence.PlayBackwards();
        }

        private Sequence SetStartButtonSequence()
        {
            var sequence = DOTween.Sequence().Pause().SetAutoKill(false).SetUpdate(true);
            sequence.Append(startGameButton.transform.DOLocalMoveX(560, startGameButtonAnimationDuration));
            var textPart = startGameButton.GetComponentInChildren<TextMeshProUGUI>();
            sequence.Insert(0, textPart.DOColor(textPart.color.ZeroAlpha(), startGameButtonAnimationDuration));
            sequence.Insert(0, startGameButton.image.DOFade(0, startGameButtonAnimationDuration));
            // sequence.AppendCallback(() => startGameButton.gameObject.SetActive(false));
        
            return sequence;
        }

        private void OnDisable()
        {
            EventBroker.OnGameSessionStarted -= OnGameSessionStarted;
            EventBroker.OnGameSessionStopped -= OnGameSessionStopped;
        }

        private void initialize()
        {
            EventBroker.OnGameSessionStarted += OnGameSessionStarted;
            EventBroker.OnGameSessionStopped += OnGameSessionStopped;
            startButtonSequence = SetStartButtonSequence();
        }
    }
}
