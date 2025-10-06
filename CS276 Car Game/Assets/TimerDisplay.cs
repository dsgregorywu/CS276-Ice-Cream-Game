// 9/23/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMeshPro namespace

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Use TextMeshProUGUI instead of Text
    private float startTime;

    private void Start()
    {
        startTime = Time.time; // Record the start time
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime; // Calculate elapsed time
        timerText.text = "Time: " + elapsedTime.ToString("F2") + "s"; // Update the text
    }
}