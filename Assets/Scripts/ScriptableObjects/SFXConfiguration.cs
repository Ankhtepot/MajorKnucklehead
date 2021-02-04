using GD.MinMaxSlider;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SFXConfiguration", menuName = "Configurations/SFX Configuration", order = 0)]
    public class SFXConfiguration : ScriptableObject
    {
        public AudioClip audioClip;
        [MinMaxSlider(0f, 1f)] public Vector2 volumeRange = new Vector2(0.3f, 0.4f);
        [MinMaxSlider(0f, 1f)] public Vector2 pitchRange = new Vector2(0.3f, 0.6f);

        public void PlayClip()
        {
            var audioSource = FindObjectOfType<AudioSource>();
            audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.PlayOneShot(audioClip);
        }
    }
}