using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;

    public Transform Orientation => orientation;
    [SerializeField] Transform orientation;

    float xRoatation;
    float yRoatation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        yRoatation += mouseX;
        xRoatation -= mouseY;
        xRoatation = Mathf.Clamp(xRoatation, -90, 90);

        transform.rotation = Quaternion.Euler(xRoatation, yRoatation, 0);
        orientation.rotation = Quaternion.Euler(0, yRoatation, 0);
    }
}
