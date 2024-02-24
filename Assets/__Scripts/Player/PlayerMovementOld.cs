using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOld : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float camUpDownSpeed = 5f;
    [SerializeField] float camLeftRightSpeed = 5f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float gravityMultiplier = 0.2f;

    Rigidbody rb;
    Camera cam;
    Vector3 movement;
    int groundCollisions = 0;

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        groundCollisions++;
    }

    private void OnTriggerExit(Collider other)
    {
        groundCollisions--;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }
    void FixedUpdate()
    {
        MoveCharacter(movement);
    }


    void Update()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector2 cameraMovement = new Vector2(mouseDelta.x * camLeftRightSpeed, mouseDelta.y * camUpDownSpeed);


        Vector3 temp = new Vector3((cam.transform.rotation.eulerAngles.x - cameraMovement.y),
            cam.transform.rotation.eulerAngles.y + cameraMovement.x,
            0);

        if (temp.x > 50f && temp.x < 90f) temp.x = 50f;
        else if (temp.x > 270 && temp.x < 310) temp.x = 310f;

        cam.transform.rotation = Quaternion.Euler(temp);
        

        /*cam.transform.rotation = Quaternion.Euler(
            (cam.transform.rotation.x - cameraMovement.y) > 45 ? 45 : 
            ((cam.transform.rotation.x - cameraMovement.y) < -45 ? -45 : (cam.transform.rotation.x - cameraMovement.y)), 
            cam.transform.rotation.y + cameraMovement.x, 
            0);*/



        movement = (cam.transform.rotation.normalized * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))).normalized;
        movement = new Vector3(movement.x, 0, movement.z).normalized;

        float prevY = movement.y;

        if (groundCollisions > 0 && Input.GetKey(KeyCode.Space))
        {
            movement = new Vector3(movement.x, jumpForce, movement.z);
        }
        else if (groundCollisions <= 0)
        {
            movement = new Vector3(movement.x, prevY + Physics.gravity.y * gravityMultiplier, movement.z);
        }
        else
        {
            movement = new Vector3(movement.x, Physics.gravity.y * 0.05f, movement.z);
        }


        if (movement.x * movement.x < 0.1f)
        {
            movement.x = 0;
        }

        if (movement.z * movement.z < 0.1f)
        {
            movement.z = 0;
        }
    }
}
