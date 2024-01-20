using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShiftCheck : MonoBehaviour
{
    public bool shiftPressed = false;
    public bool doubleShift = false;
    private float doubleTapTimeThreshold = 0.2f; // Adjust as needed
    private float lastShiftPressTime = 0f;

    private void Update()
    {
        // Check if the left shift key is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!shiftPressed)
            {
                shiftPressed = true;

                // Check if it's a double tap
                if (Time.time - lastShiftPressTime <= doubleTapTimeThreshold)
                {
                    // Double tap detected
                    Debug.Log("Double tap detected!");
                    doubleShift = true;
                }

                lastShiftPressTime = Time.time;
            }
        }

        // Reset the shiftPressed flag when the key is released
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftPressed = false;
        }
    }
}