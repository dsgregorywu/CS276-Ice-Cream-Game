// 9/23/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Import the TextMeshPro namespace
using VictoryAudio; // Namespace for VictorySoundManager
using DeliveryAudio; // Namespace for IceCreamDeliverySound

public class IceCreamDelivery : MonoBehaviour
{
    [SerializeField] VictorySoundManager victorySoundManager; // Assign in Inspector
    [SerializeField] DeliverySoundManager deliverySoundManager; // Assign in Inspector
    public Cruise carMovementScript; // Assign the car's movement script in the Inspector
    public string currentIceCream = null; // Tracks the currently held ice cream
    public bool hasIceCream = false;
    public float deliveryDistance = 1.5f; // Distance within which delivery is successful
    private bool bluedone = false;
    private bool reddone = false;
    private bool pinkdone = false;
    private bool yellowdone = false;

    public GameObject confettiPrefab; // Assign a confetti particle prefab in the Inspector
    public ParticleSystem houseRedEffect; // Assign the HouseRed particle effect in the Inspector
    public ParticleSystem houseBlueEffect; // Assign the HouseBlue particle effect in the Inspector
    public ParticleSystem housePinkEffect; // Assign the HousePink particle effect in the Inspector
    public ParticleSystem houseYellowEffect; // Assign the HouseYellow particle effect in the Inspector
    public TextMeshProUGUI victoryTimeText; // Assign the Victory Time Text in the Inspector
    public TextMeshProUGUI timerText; // Use TextMeshProUGUI instead of Text
    public GameObject victoryScreen; // Assign the Victory Screen UI Panel in the Inspector
    public Button restartButton; // Assign the Restart Button in the Inspector

    private float startTime;
    private bool gameEnded = false;

    private void Start()
    {
        startTime = Time.time; // Record the start time of the game

        // Ensure the victory screen is hidden at the start
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }

        // Add a listener to the restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    private void Update()
    {
        if (!gameEnded)
        {
            // Update the timer display
            float elapsedTime = Time.time - startTime;
            if (timerText != null)
            {
                timerText.text = "Time: " + elapsedTime.ToString("F2") + "s";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Picking up ice cream
        if ((collision.CompareTag("IceCreamRed") || collision.CompareTag("IceCreamBlue") || collision.CompareTag("IceCreamPink") || collision.CompareTag("IceCreamYellow")) && !hasIceCream)
        {
            currentIceCream = collision.gameObject.name; // Assume the ice cream object is named by color
            hasIceCream = true;

            Destroy(collision.gameObject); // Remove the ice cream from the scene
        }

        // Delivering ice cream
        if (hasIceCream && collision.CompareTag(currentIceCream + "House"))
        {
            Debug.Log("Delivered to " + currentIceCream + " house!");
            if (currentIceCream == "IceCreamRed")
            {
                reddone = true;
                houseRedEffect?.Play(); // Play the red house particle effect
            }
            if (currentIceCream == "IceCreamBlue")
            {
                bluedone = true;
                houseBlueEffect?.Play(); // Play the blue house particle effect
            }
            if (currentIceCream == "IceCreamPink")
            {
                pinkdone = true;
                housePinkEffect?.Play(); // Play the pink house particle effect
            }
            if (currentIceCream == "IceCreamYellow")
            {
                yellowdone = true;
                houseYellowEffect?.Play(); // Play the yellow house particle effect
            }
            deliverySoundManager.PlayDeliverySound();
            hasIceCream = false;
            currentIceCream = null;

            // Check win condition
            if (reddone && bluedone && pinkdone && yellowdone)
            {
                Debug.Log("All ice creams delivered! You win!");
                EndGame();
            }
        }
    }

 // 9/23/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

private void EndGame()
{
    gameEnded = true; // Stop the timer updates
    float finalTime = Time.time - startTime; // Calculate the final time

    // Disable the car's movement script
    if (carMovementScript != null)
    {
        carMovementScript.enabled = false;
    }

    // Hide the timer text
    if (timerText != null)
    {
        timerText.gameObject.SetActive(false);
    }

    // Show the victory screen
    if (victoryScreen != null)
    {
        victoryScreen.SetActive(true);
    }

    // Display the final time on the victory screen
    if (victoryTimeText != null)
    {
        victoryTimeText.text = "Final Time: " + finalTime.ToString("F2") + "s";
    }

    victorySoundManager.PlayVictorySound();

    Debug.Log("Game Over: You delivered all the ice creams!");
}

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

// 9/23/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.
