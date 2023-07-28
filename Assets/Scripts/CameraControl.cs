using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Settings")]
    public int sensitivity = 400;
    public float verticalLimit = 75f;

    [Header("Objects")]
    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Make the cursor invisible and lock it in its current position,
         * so that the user does not run into the screen boundaries
         * when trying to use the cursor to rotate the camera.
         */
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Get theoretical mouse location
         */
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        /*
         * Set rotation variables.
         * 
         * Also, stop the player from looking more than 90d
         * up or down, as this would cause their view to appear upside-down.
         */
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLimit, verticalLimit);

        yRotation += mouseX;

        /*
         * Apply rotation variables.
         */
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
