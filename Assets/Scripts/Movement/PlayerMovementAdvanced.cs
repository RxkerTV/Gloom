using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementAdvanced : SingletonMonoBehaviour<PlayerMovementAdvanced>
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
    public KeyCode crawlKey = KeyCode.C;

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

    public CapsuleCollider playerCollider;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;

    public bool canMove;

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

        originalColliderHeight = playerCollider.height;
        originalColliderCenter = playerCollider.center;

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

        // Handle drag
        rb.drag = grounded ? groundDrag : 0;
    }

    private void GroundCheck()
    {
        grounded = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            if (state == MovementState.jumping && !hasLanded)
            {
                hasLanded = true;
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
            rb.AddForce(Physics.gravity * rb.mass, ForceMode.Force);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey) && grounded)
        {
            Crouch();
        }

        if (Input.GetKeyDown(crawlKey) && grounded)
        {
            Crawl();
        }

        if (Input.GetKeyUp(crouchKey) || Input.GetKeyUp(crawlKey))
        {
            StandUp();
        }
    }

    private void Crouch()
    {
        state = MovementState.crouching;
        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        playerCollider.height = originalColliderHeight * crouchYScale;
        playerCollider.center = new Vector3(playerCollider.center.x, originalColliderCenter.y - (originalColliderHeight - playerCollider.height) / 2, playerCollider.center.z);
        moveSpeed = crouchSpeed;
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    private void Crawl()
    {
        state = MovementState.crawling;
        transform.localScale = new Vector3(transform.localScale.x, crawlYScale, transform.localScale.z);
        playerCollider.height = originalColliderHeight * crawlYScale;
        playerCollider.center = new Vector3(playerCollider.center.x, originalColliderCenter.y - (originalColliderHeight - playerCollider.height) / 2, playerCollider.center.z);
        moveSpeed = crawlSpeed;
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    private void StandUp()
    {
        if (!Input.GetKey(crouchKey) && !Input.GetKey(crawlKey))
        {
            // Check if there is enough space above to stand
            if (CanStandUp())
            {
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
                playerCollider.height = originalColliderHeight;
                playerCollider.center = originalColliderCenter;

                if (Input.GetKey(sprintKey))
                {
                    state = MovementState.sprinting;
                    moveSpeed = sprintSpeed;
                }
                else
                {
                    state = MovementState.walking;
                    moveSpeed = walkSpeed;
                }
            }
        }
    }

    private bool CanStandUp()
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * (playerCollider.height / 2);

        // Cast a ray upwards from the player's position
        if (Physics.Raycast(rayStart, Vector3.up, out hit, 2f, whatIsGround))
        {
            return false; // There is an object above, so the player can't stand up
        }

        return true; // No objects above, the player can stand up
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
            }
            else if (Input.GetKey(crouchKey))
            {
                state = MovementState.crouching;
                desiredMoveSpeed = crouchSpeed;
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
            desiredMoveSpeed = moveSpeed;
        }

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
        if (canMove == true)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (state == MovementState.jumping || !grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
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

            rb.useGravity = !OnSlope();
        }
    }

    private void PlayerCanMove()
    {
        if (PlayerCam.Instance.InventoryOn==true)
            canMove = false;
        if (LookMode.Instance.PauseMenuOn==true)
            canMove = false;
        if (PlayerCam.Instance.InventoryOn == false)
            canMove = true;
        if (LookMode.Instance.PauseMenuOn == false)
            canMove = true;
    }
    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        if (!PlayerCam.Instance.InventoryOn && !LookMode.Instance.PauseMenuOn)
        {
            if (grounded)
            {
                exitingSlope = true;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
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
