// 9/23/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using TMPro;

public class TextMeshProOutlinerAdder : MonoBehaviour
{
    void Start()
    {
        // Find all TextMeshPro objects in the scene
        TextMeshProUGUI[] tmpTexts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var tmpText in tmpTexts)
        {
            // Enable outline
            tmpText.fontMaterial.EnableKeyword("OUTLINE_ON");
            tmpText.outlineColor = Color.black;
            tmpText.outlineWidth = 0.2f; // Adjust width as needed
        }
    }
}