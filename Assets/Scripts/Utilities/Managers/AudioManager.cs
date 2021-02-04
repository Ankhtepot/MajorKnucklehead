using System.Collections.Generic;
using System.Linq;
using Enumerations;
using ScriptableObjects;
using UnityEngine;

namespace Utilities.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SFXConfiguration[] SfxConfigurations;

        private Dictionary<AudioClipPurpose, List<SFXConfiguration>> jukebox;

        private void Awake()
        {
            EventBroker.OnPlayAudioRequested += OnPlayAudioRequested;
            jukebox = new Dictionary<AudioClipPurpose, List<SFXConfiguration>>();
            SortSfxConfigurations();
        }

        private void SortSfxConfigurations()
        {
            foreach (var configuration in SfxConfigurations)
            {
                if (!configuration.audioClip)
                {
                    Debug.LogWarning($"SFX Configuration \"{configuration.name}\" has no audio clip assigned!");
                    continue;
                }
                
                if (!jukebox.ContainsKey(configuration.clipPurpose))
                {
                    jukebox.Add(configuration.clipPurpose, new List<SFXConfiguration>());
                }
                
                jukebox[configuration.clipPurpose].Add(configuration);
            }
        }

        private void OnPlayAudioRequested(AudioClipPurpose purpose)
        {
            jukebox[purpose][Random.Range(0, jukebox[purpose].Count)].PlayClip(audioSource);
        }

        private void OnDisable()
        {
            EventBroker.OnPlayAudioRequested -= OnPlayAudioRequested;
        }
    }
}