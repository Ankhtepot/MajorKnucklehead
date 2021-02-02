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
        [Header("Score Text")] 
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private float scoreTextAnimationDuration = 1f;

        [HideInInspector] public GameSessionManager gameSessionManager;
        
        private Sequence startButtonSequence;
        private Sequence scoreTextSequence;
        private Sequence DefaultSequence => DOTween.Sequence().Pause().SetAutoKill(false).SetUpdate(true);
#pragma warning restore 649

        void Start()
        {
            initialize();
        }

        public void OnStartGameSessionButtonClicked() => EventBroker.TriggerOnGameSessionStartRequested();

        public void OnStopGameSessionButtonClicked() => EventBroker.TriggerOnGameSessionStopRequested();

        private void OnGameSessionStarted()
        {
            startGameButton.interactable = false;
            startButtonSequence.Restart();   
            scoreTextSequence.Restart();
        }
    
        private void OnGameSessionStopped()
        {
            startGameButton.gameObject.SetActive(true);
            startButtonSequence.PlayBackwards();
            scoreTextSequence.PlayBackwards();
            startGameButton.interactable = true;
        }

        private Sequence SetStartButtonSequence()
        {
            var sequence = DefaultSequence;
            sequence.Append(startGameButton.transform.DOLocalMoveX(560, startGameButtonAnimationDuration));
            var textPart = startGameButton.GetComponentInChildren<TextMeshProUGUI>();
            sequence.Insert(0, textPart.DOColor(textPart.color.ZeroAlpha(), startGameButtonAnimationDuration));
            sequence.Insert(0, startGameButton.image.DOFade(0, startGameButtonAnimationDuration));

            return sequence;
        }

        private Sequence SetTMPSequence(TextMeshProUGUI tmp, float animationDuration)
        {
            var sequence = DefaultSequence;
            sequence.Insert(0, tmp.DOFade(1, animationDuration));
            sequence.Insert(0, tmp.transform.DOLocalMoveX(-288, animationDuration));

            return sequence;
        }

        private void OnScoreChanged(int newScore)
        {
            scoreTMP.text = newScore.ToString();
        }

        private void OnDisable()
        {
            EventBroker.OnGameSessionStarted -= OnGameSessionStarted;
            EventBroker.OnGameSessionStopped -= OnGameSessionStopped;
            EventBroker.OnScoreChanged -= OnScoreChanged;
        }

        private void initialize()
        {
            EventBroker.OnGameSessionStarted += OnGameSessionStarted;
            EventBroker.OnGameSessionStopped += OnGameSessionStopped;
            EventBroker.OnScoreChanged += OnScoreChanged;
            startButtonSequence = SetStartButtonSequence();
            scoreTextSequence = SetTMPSequence(scoreTMP, scoreTextAnimationDuration);
        }
    }
}
