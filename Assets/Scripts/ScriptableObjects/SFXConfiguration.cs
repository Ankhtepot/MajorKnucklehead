using Enumerations;
using GD.MinMaxSlider;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SFXConfiguration", menuName = "Configurations/SFX Configuration", order = 0)]
    public class SFXConfiguration : ScriptableObject
    {
        public AudioClip audioClip;
        public AudioClipPurpose clipPurpose;
        [MinMaxSlider(0f, 1f)] public Vector2 volumeRange = new Vector2(0.3f, 0.4f);
        [MinMaxSlider(0f, 1f)] public Vector2 pitchRange = new Vector2(0.3f, 0.6f);

        private AudioSource audioSource;

        public void PlayClip(AudioSource AS = null)
        {
            if (AS)
            {
                audioSource = AS;
            }

            if (!audioSource)
            {
                audioSource = FindObjectOfType<AudioSource>();
            }

            if (audioSource is null) return;
            
            audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.PlayOneShot(audioClip);
        }
    }
}