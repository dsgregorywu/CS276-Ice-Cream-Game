// 9/19/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Required for UI components
using SpinOutSound; // Namespace for SpinOutSound

public class Cruise : MonoBehaviour
{
    public bool isControlEnabled = true;
    SpinOutSound.SpinOutSound spinOutSoundScript; // Reference to SpinOutSound script
    void Awake()
    {
        // Try to find the SpinOutSound script in the scene
        spinOutSoundScript = FindObjectOfType<SpinOutSound.SpinOutSound>();
        if (spinOutSoundScript == null)
        {
            Debug.LogError("SpinOutSound script not found in the scene!");
        }
    }
    float currentSpeed = 5f;
    float steeringSpeed = 200f;
    float offRoadSpeed = 3f; // Speed when on grass
    float normalSpeed = 5f;  // Default speed on non-grass areas
    bool isOnGrass = false;  // Tracks whether the car is on grass

    // Turbo system variables
    public float turboSpeed = 15f; // Speed during turbo
    public float turboDuration = 5f; // Maximum turbo time
    public float turboRechargeRate = 2f; // Recharge rate per second
    private float turboMeter; // Current turbo meter value
    public bool isTurboActive = false; // Tracks if turbo is active
    private bool isSpinningOut = false; // Tracks if the car is spinning out
    private bool turboLocked = false; // Tracks if turbo is locked after spin-out

    // Reference to the Turbo Meter UI Slider
    public Slider turboSlider;

    void Start()
    {
        Debug.Log("Car script initialized!");
        turboMeter = turboDuration; // Initialize turbo meter to full

        // Ensure the turboSlider is assigned
        if (turboSlider == null)
        {
            Debug.LogError("Turbo Slider is not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect collision with static objects while turbo is active
        if (
            isTurboActive && (
                collision.gameObject.isStatic ||
                collision.CompareTag("StaticNature") ||
                collision.CompareTag("House") ||
                collision.CompareTag("IceCreamBlueHouse") ||
                collision.CompareTag("IceCreamYellowHouse") ||
                collision.CompareTag("IceCreamRedHouse") ||
                collision.CompareTag("IceCreamPinkHouse")
            )
        )
        {
            StartCoroutine(SpinOut());
        }

        if (collision.CompareTag("Grass")) // Detect entering grass
        {
            isOnGrass = true;
            currentSpeed = offRoadSpeed;
            Debug.Log("On grass! Speed reduced to: " + currentSpeed);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass")) // Detect exiting grass
        {
            isOnGrass = false;
            currentSpeed = normalSpeed;
            Debug.Log("Off grass! Speed restored to: " + currentSpeed);
        }
    }

    void Update()
    {
        // Prevent movement and turbo activation during spin-out
        if (isSpinningOut || !isControlEnabled) return;

        // Handle turbo activation
        if (Keyboard.current.leftShiftKey.isPressed && turboMeter > 0 && !turboLocked)
        {
            isTurboActive = true;
            currentSpeed = turboSpeed;
            turboMeter -= Time.deltaTime * 2; // Deplete turbo meter

            if (turboMeter <= 0)
            {
                turboMeter = 0;
                isTurboActive = false;
                turboLocked = true; // Lock turbo after depletion
                StartCoroutine(SpinOut()); // Trigger spin-out when turbo runs out
            }
        }
        else
        {
            isTurboActive = false;
            currentSpeed = isOnGrass ? offRoadSpeed : normalSpeed; // Restore normal or off-road speed

            if (turboLocked)
            {
                turboMeter += Time.deltaTime * turboRechargeRate; // Recharge turbo meter
                if (turboMeter >= turboDuration)
                {
                    turboMeter = turboDuration; // Ensure turbo meter is full
                    turboLocked = false; // Unlock turbo when fully recharged
                }
            }
            else
            {
                turboMeter += Time.deltaTime * turboRechargeRate; // Recharge turbo meter
            }
        }

        // Clamp turbo meter between 0 and max duration
        turboMeter = Mathf.Clamp(turboMeter, 0, turboDuration);

        // Update the Turbo Meter UI
        if (turboSlider != null)
        {
            turboSlider.value = turboMeter / turboDuration; // Normalize the value between 0 and 1
        }

        // Movement code
        float move = 0f;
        float steer = 0f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            move = 1f;
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            move = -1f;
        }

        // Allow steering at any time
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            steer = 1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            steer = -1f;
        }

        float moveAmount = move * Time.deltaTime * currentSpeed;
        float steerAmount = steer * Time.deltaTime * steeringSpeed;

        // Move forward/backward in local space
        transform.Translate(Vector3.up * moveAmount);
        // Rotate around Z axis (2D car)
        transform.Rotate(0, 0, steerAmount);
    }

    IEnumerator SpinOut()
    {
        isSpinningOut = true;
        isTurboActive = false; // Disable turbo during spin-out
        float spinDuration = 2f; // Duration of the spin-out
        float spinSpeed = 360f; // Speed of the spin (degrees per second)
        spinOutSoundScript.PlaySpinOutSound(); // Play spin-out sound
        Debug.Log("Spinning out!");

        // Spin the car in a circle
        float elapsedTime = 0f;
        while (elapsedTime < spinDuration)
        {
            transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Spin-out complete. Regaining control.");

        // Restore control after spin-out
        isSpinningOut = false;
    }
}