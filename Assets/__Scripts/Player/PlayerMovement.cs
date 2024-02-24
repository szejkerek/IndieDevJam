using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Camera cam;



    int groundCollisions = 0;


    Vector2 previousMousePosition;



    [SerializeField]
    private float speed = 10f, camUpDownSpeed = 5f, camLeftRightSpeed = 5f, jumpForce = 50f, gravityMultiplier = 0.2f;
    private Vector3 movement;


    private void OnTriggerEnter(Collider other)
    {
        groundCollisions += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        groundCollisions -= 1;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        Vector2 cameraMovement = new Vector2((Input.mousePosition.x - previousMousePosition.x) * Time.fixedDeltaTime * camLeftRightSpeed,
    (Input.mousePosition.y - previousMousePosition.y) * Time.fixedDeltaTime * camUpDownSpeed);


        cam.transform.rotation = Quaternion.Euler(
            (cam.transform.rotation.x - cameraMovement.y) > 45 ? 45 : 
            ((cam.transform.rotation.x - cameraMovement.y) < -45 ? -45 : (cam.transform.rotation.x - cameraMovement.y)), 
            cam.transform.rotation.y + cameraMovement.x, 
            0);

        

        movement = (cam.transform.rotation.normalized * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))).normalized;
        movement = new Vector3(movement.x, 0, movement.z).normalized;

        float prevY = movement.y;

        if (groundCollisions > 0 && Input.GetKey(KeyCode.Space)) movement = new Vector3(movement.x, jumpForce, movement.z);
        else if (groundCollisions <= 0) movement = new Vector3(movement.x, prevY + Physics.gravity.y * gravityMultiplier, movement.z);
        else movement = new Vector3(movement.x, Physics.gravity.y * 0.05f, movement.z);

        if (movement.x * movement.x < 0.1f) movement.x = 0;
        if (movement.z * movement.z < 0.1f) movement.z = 0;
    }


    void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }
}
