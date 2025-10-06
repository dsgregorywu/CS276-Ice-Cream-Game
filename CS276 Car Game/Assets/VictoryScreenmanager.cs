// 9/23/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class VictoryScreenManager : MonoBehaviour
{
    public GameObject victoryScreen; // Assign the VictoryScreen GameObject in the Inspector
    public GameObject car; // Assign the car GameObject in the Inspector

    private Cruise cruiseScript;

    void Start()
    {
        if (car != null)
        {
            cruiseScript = car.GetComponent<Cruise>();
        }
    }

    public void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true); // Show the victory screen
        }

        if (cruiseScript != null)
        {
            cruiseScript.isControlEnabled = false; // Disable car control
        }
    }

    public void HideVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false); // Hide the victory screen
        }

        if (cruiseScript != null)
        {
            cruiseScript.isControlEnabled = true; // Enable car control
        }
    }
}