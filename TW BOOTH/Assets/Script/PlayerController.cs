using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform head;
    public Camera cam;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f; // Sensitivity for mouse movement
    public float maxLookAngle = 80f;    // Limit for looking up/down

    private float rotationX = 0f;       // To store vertical rotation of the camera

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotatePlayerHead();   // Handles head rotation with mouse
    }

    private void FixedUpdate()
    {
        MovePlayer();         // Handles player movement with WASD
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 newVelocity = moveDirection.normalized * speed;
        newVelocity.y = rb.velocity.y;  // Preserve the current Y velocity for gravity

        rb.velocity = newVelocity;
    }

    private void RotatePlayerHead()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player body horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Adjust the vertical rotation of the head
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);  // Clamp rotation to avoid over-rotation

        // Apply vertical rotation to the head
        head.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
