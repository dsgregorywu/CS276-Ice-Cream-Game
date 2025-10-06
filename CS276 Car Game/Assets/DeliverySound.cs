// 10/5/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
namespace DeliveryAudio
{
    public class DeliverySoundManager : MonoBehaviour
    {
        public AudioClip deliverySound; // Assign the delivery sound effect in the Inspector
        private AudioSource audioSource;
        private int deliveryCount = 0; // Tracks the number of deliveries
        private int maxDeliveries = 3; // Maximum number of deliveries to play the sound

        void Start()
        {
            // Add an AudioSource component to this GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlayDeliverySound()
        {
            if (deliveryCount < maxDeliveries && deliverySound != null)
            {
                audioSource.PlayOneShot(deliverySound);
                deliveryCount++;
            }
        }
    }
}