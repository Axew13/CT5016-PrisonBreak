using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Walking")]
    public float speed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool jumpReady = true;

    [Header("Ground Check")]
    public float playerHeight;
    public GameObject groundRayFront;
    public GameObject groundRayBack;
    public LayerMask groundLayer;
    bool grounded;

    [Header("Stepping")]
    public GameObject stepRayUpper;
    public GameObject stepRayLower;
    public float stepHeight = 0.3f;
    public float stepSmooth = 0.1f;

    [Header("Other")]
    public Transform orientation;

    float hInput;
    float vInput;
    Vector3 direction;
    Rigidbody rb;

    private void Start ()
    {
        rb = this.GetComponentInChildren<Rigidbody>();
        rb.freezeRotation = true;

        /*
         * Set step raycast object to step height serialised value
         */
        Vector3 stepRayUpperPosition = stepRayUpper.transform.position;
        stepRayUpperPosition.y = stepHeight;
        stepRayUpper.transform.position = stepRayUpperPosition;
    }

    public void Update ()
    {
        /*
         * Use raycasting to check if there is ground below
         */
        if (Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, groundLayer) ||
            Physics.Raycast(groundRayFront.transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, groundLayer) ||
            Physics.Raycast(groundRayBack.transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, groundLayer))
        {
            grounded = true;
        }

        RecieveInput();
        RunSpeedLimiter();

        /*
         * Apply drag
         */
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    public void FixedUpdate ()
    {
        RunMovement();

        RunSteppingUp();
    }

    private void RecieveInput ()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && jumpReady && grounded)
        {
            jumpReady = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void RunMovement ()
    {
        /*
         * Detect input. Handles joysticks and WASD
         */
        direction = orientation.forward * vInput + orientation.right * hInput;

        /*
         * Calculate drag depending on whether grounded or in the air
         */
        if (grounded)
        {
            rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(direction.normalized * speed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void RunSteppingUp ()
    {
        /*
         * Check if something is directly ahead
         */
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {

            /*
             * Make sure it's not a wall or something too high to step up
             */
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    private void RunSpeedLimiter ()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        /*
         * Limit velocity to speed
         */
        if (velocity.magnitude > speed)
        {
            Vector3 limitedVelocity = velocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump ()
    {
        /*
         * Reset Y velocity to 0, so I always jump the same height
         */
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        /*
         * Apply force for jump. Use ForceMode.Impulse as the force is being applied as an instant pulse
         */
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump ()
    {
        jumpReady = true;
    }
}