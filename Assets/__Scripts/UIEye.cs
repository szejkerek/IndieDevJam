using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEye : MonoBehaviour
{
    public float offset = 18;
    Image eye;

    void Start()
    {
        eye = GetComponent<Image>();

        // Check if Image component is present
        if (eye == null)
        {
            Debug.LogError("Image component not found on the UIEye object.");
            return;
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 lookDirection = mousePos - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Rotate the eye towards the mouse
        eye.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + offset));

    }
}
