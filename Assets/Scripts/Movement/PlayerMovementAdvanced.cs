using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAdvanced : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public object reach;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    private bool hasLanded = false;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Crawling")]
    public float crawlSpeed;
    public float crawlYScale;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    bool walled;
    public float groundCheckRadius = 0.4f;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Sounds")]
    public PlayerSound FootSound;
    public PlayerSound playerSound;



    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    private Vector3 _velocity;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        sliding,
        air,
        jumping,
        crawling
    }

    public bool sliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        GroundCheck();
        Wallcheck();

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void GroundCheck()
    {
        grounded = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            if (state == MovementState.jumping && !hasLanded)
            {
                // Player has landed
                hasLanded = true;
                // Play landing sound effect
                //PlayerSound.PlayLandingSound();

                // Optionally, reset state
                state = MovementState.walking; // Or any other suitable state
            }
        }
        else
        {
            hasLanded = false; // Reset landing status when not grounded
        }
    }
    private void Wallcheck()
    {
        walled = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.forward, out RaycastHit hit, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (walled)
        {
            //Debug.Log("Grounded on: " + hit.collider.name + " with layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
        }
        else
        {
            // Debug.Log("Not grounded");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ApplyGravity();
    }
    private void ApplyGravity()
    {
        if (state == MovementState.jumping || !grounded)
        {
            // Apply gravity manually for better control
            rb.AddForce(Physics.gravity * rb.mass, ForceMode.Force);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Handle jumping
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Handle crouching
        if (Input.GetKeyDown(crouchKey) && !Input.GetKey(KeyCode.C)) // Only crouch if C is not pressed
        {
            // Start crouching
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // Handle crawling
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Start crawling
            transform.localScale = new Vector3(transform.localScale.x, crawlYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // Stop crouching or crawling
        if (Input.GetKeyUp(crouchKey) || Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }


    private void StateHandler()
    {
        if (PlayerCam.Instance.InventoryOn || LookMode.Instance.PauseMenuOn)
            return;

        if (grounded)
        {
            if (Input.GetKey(KeyCode.C))
            {
                state = MovementState.crawling;
                desiredMoveSpeed = crawlSpeed;
                Debug.Log("Crawl");
            }
            else if (Input.GetKey(crouchKey))
            {
                state = MovementState.crouching;
                desiredMoveSpeed = crouchSpeed;
                Debug.Log("Crouch");
            }
            else if (Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                desiredMoveSpeed = sprintSpeed;
            }
            else if (sliding)
            {
                state = MovementState.sliding;
                desiredMoveSpeed = OnSlope() && rb.velocity.y < 0.1f ? slideSpeed : sprintSpeed;
            }
            else
            {
                state = MovementState.walking;
                desiredMoveSpeed = walkSpeed;
            }
        }
        else
        {
            state = MovementState.air;
            desiredMoveSpeed = moveSpeed; // Keep air speed consistent
        }

        // Lerp the movement speed if necessary
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        if (PlayerCam.Instance.InventoryOn == false && LookMode.Instance.PauseMenuOn == false)
        {
            // Calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            // Handle different states
            if (state == MovementState.jumping || !grounded)
            {
                // Move in the air
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

                // No footstep sounds in the air
            }
            else if (OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

                if (rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);

                if (moveDirection.magnitude > 0.1f)
                {
                    FootSound.PlayFootStep(rb.velocity);
                }
            }
            else if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

                if (moveDirection.magnitude > 0.1f)
                {
                    FootSound.PlayFootStep(rb.velocity);
                }
            }

            else if (state == MovementState.crawling)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

                if (moveDirection.magnitude > 0.1f)
                {
                    FootSound.PlayFootStep(rb.velocity);
                }
            }

            // Turn gravity off while on slope
            rb.useGravity = !OnSlope();
        }
    }


    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        if (PlayerCam.Instance.InventoryOn == false && LookMode.Instance.PauseMenuOn == false)
        {
            // Ensure the player is grounded before jumping
            if (grounded)
            {
                exitingSlope = true;

                // Reset vertical velocity
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                // Set state to jumping
                state = MovementState.jumping;
            }
        }
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
    // Assign a specific layer to the reach object

}