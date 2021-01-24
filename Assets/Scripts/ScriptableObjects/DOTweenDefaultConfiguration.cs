using DG.Tweening;
using Interface;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "DOTWeenDefaultConfiguration", menuName = "Configurations/DOTweenDefaultConfiguration", order = 0)]
    public class DOTweenDefaultConfiguration : ConfigurationBase
    {
        public Ease defaultEase;
        public bool defaultKillOnFinish;

        public override void ActivateConfiguration()
        {
            DOTween.defaultEaseType = defaultEase;
            DOTween.defaultAutoKill = defaultKillOnFinish;
        }
    }
}