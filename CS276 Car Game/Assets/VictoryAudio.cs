// 10/5/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

namespace VictoryAudio
{
    public class VictorySoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip victorySound; // Assign the sound effect in the Inspector
        private AudioSource audioSource;
        [SerializeField] private GameObject backgroundMusicObject; // Reference to the BackgroundMusic GameObject

        void Start()
        {
            // Add an AudioSource component to this GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlayVictorySound()
        {
            if (victorySound != null)
            {
                // Stop the background music
                if (backgroundMusicObject != null)
                {
                    AudioSource bgAudioSource = backgroundMusicObject.GetComponent<AudioSource>();
                    if (bgAudioSource != null)
                    {
                        bgAudioSource.Stop();
                    }
                }

                // Play the victory sound
                audioSource.PlayOneShot(victorySound);
            }
        }
    }
}