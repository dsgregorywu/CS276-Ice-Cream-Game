// 10/5/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
namespace SpinOutSound
{
    public class SpinOutSound : MonoBehaviour
    {
        public AudioClip spinOutSound; // Assign the spin-out sound effect in the Inspector
        private AudioSource audioSource;

        void Start()
        {
            // Add an AudioSource component to this GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlaySpinOutSound()
        {
            if (spinOutSound != null)
            {
                audioSource.PlayOneShot(spinOutSound);
            }
        }
    }
}