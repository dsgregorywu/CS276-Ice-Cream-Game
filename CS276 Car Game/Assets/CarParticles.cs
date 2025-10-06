// 9/23/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class CarParticleController : MonoBehaviour
{
    public ParticleSystem carTrail; // Assign the particle system in the Inspector
    private ParticleSystem.MainModule mainModule;

    private IceCreamDelivery deliveryScript; // Reference to the Delivery script
    private Cruise cruiseScript; // Reference to the Cruise script

    private void Start()
    {
        if (carTrail != null)
        {
            mainModule = carTrail.main;
        }

        // Automatically find the Delivery and Cruise scripts on the same GameObject
        deliveryScript = GetComponent<IceCreamDelivery>();
        cruiseScript = GetComponent<Cruise>();

        if (deliveryScript == null)
        {
            Debug.LogError("IceCreamDelivery script not found on the GameObject!");
        }

        if (cruiseScript == null)
        {
            Debug.LogError("Cruise script not found on the GameObject!");
        }
    }

    private void Update()
    {
        if (deliveryScript != null)
        {
            // Change particle color based on the ice cream color
            string currentIceCream = deliveryScript.currentIceCream;

            if (currentIceCream == "IceCreamRed")
            {
                mainModule.startColor = Color.red;
            }
            else if (currentIceCream == "IceCreamBlue")
            {
                mainModule.startColor = Color.blue;
            }
            else if (currentIceCream == "IceCreamPink")
            {
                mainModule.startColor = new Color(1f, 0.4f, 0.7f); // Pink
            }
            else if (currentIceCream == "IceCreamYellow")
            {
                mainModule.startColor = Color.yellow;
            }
            else
            {
                mainModule.startColor = Color.grey; // Default color
            }
        }

        if (cruiseScript != null)
        {
            // Change particle size when in turbo mode
            if (cruiseScript.isTurboActive)
            {
                mainModule.startSize = 0.5f; // Larger particles
            }
            else
            {
                mainModule.startSize = 0.2f; // Normal size
            }
        }
    }
}