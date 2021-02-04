

//Fireball Games * * * PetrZavodny.com

using System;
using System.Collections;
using DTOs;
using Enumerations;
using ScriptableObjects;
using UnityEngine;
using Utilities;
using Utilities.Managers;

namespace Actors.Enemies
{
    public class SpaceGateSpawner : SpawnerMono
    {
#pragma warning disable 649
        [SerializeField] protected float birthDelay = 1f;
        [SerializeField] protected float activationDelay = 1f;
        [SerializeField] protected float endLifeCycleDelay = 2f;
        [SerializeField] private DestructStyle destructStyle;
        [SerializeField] private GameObject portalEffect;
#pragma warning restore 649

        protected override void Awake()
        {
            base.Awake();
            StartSpawningWaves();
        }

        protected override void StartSpawningWaves()
        {
            StartCoroutine(InitializePortalRoutine());
        }

        private IEnumerator InitializePortalRoutine()
        {
            yield return new WaitForSeconds(birthDelay);
            
            // TODO: Process appearance of a portal
            
            yield return new WaitForSeconds(activationDelay);
            
            portalEffect.SetActive(true);
            
            base.StartSpawningWaves();
        }
        
        protected override IEnumerator SpawnEnemySequence(WaveConfiguration waveConfiguration)
        {
            yield return base.SpawnEnemySequence(waveConfiguration);

            DestructGate();

            yield return null;
        }

        private void DestructGate()
        {
            switch(destructStyle)
            {
                case DestructStyle.SwitchOff:
                    break;
                case DestructStyle.Disappear:
                    break;
                case DestructStyle.Explode:
                    break;
                case DestructStyle.Unregister:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            EventBroker.TriggerOnSpawnerUnregister(); // TODO: will need to move to respective cases because of timing of those events
        }
    }
}
