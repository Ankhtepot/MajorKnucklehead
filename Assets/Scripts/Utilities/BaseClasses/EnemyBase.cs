using System;
using UnityEngine;

namespace Utilities.BaseClasses
{
    public class EnemyBase : ExtendedMono
    {
        [SerializeField] protected int scoreValue = 10;

        protected virtual void OnDisable()
        {
            EventBroker.TriggerOnScoreGained(scoreValue);
        }
    }
}