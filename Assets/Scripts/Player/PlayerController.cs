using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Character Movement Properties
    [Tooltip("Maximum speed that the character moves (units per second)")]
    public readonly float maxHorizontalSpeed = 15;
    [Tooltip("Maximum vertical speed that the character moves (units per second)")]
    public readonly float maxVerticalSpeed = 5;
    [Tooltip("Acceleration whilst grounded")]
    public readonly float runAcceleration = 75;
    [Tooltip("Acceleration whilst in the air")]
    public readonly float airAcceleration = 70;
    [Tooltip("Deceleration applied when grounded and not attempting to move")]
    public readonly float groundDeceleration = 70;

    [Tooltip("Max height the character will jump")]
    public readonly float jumpHeight = 5;

    [Tooltip("The duration of the wall jump")]
    public readonly float wallJumpDuration = 0.2f;
    [Tooltip("The horizontal velocity to apply whilst wall jumping")]
    public readonly float wallJumpSpeed = 13;
    [Tooltip("The height of the wall jump")]
    public readonly float wallJumpHeight = 1;
    [Tooltip("Fastest speed allowed to slide down walls")]
    public readonly float wallSlideDownSpeed = -12;

    [Tooltip("The velocity to apply whilst dashing")]
    public readonly float dashSpeed = 25;
    [Tooltip("The duration the dash lasts")]
    public readonly float dashDuration = 0.25f;

    [SerializeField]
    public int remainingDashes;
    #endregion

    #region State Machine - slam this into its own class for future use
    public IdleState idleState;
    public RunState runState;
    public JumpState jumpState;
    public WallSlidingState wallSlidingState;
    public WallJumpState wallJumpState;
    public FallState fallState;
    public DashState dashState;
    public DeathState deathState;
    public RespawnState respawnState;

    private List<PlayerState> playerStates = new List<PlayerState>();
    private PlayerState currentState;
    #endregion

    #region Components
    public CameraController cameraController;
    [SerializeField] LayerMask groundLayer;
    public ParticleSystem dustPrefab;
    #endregion

    #region Tags
    private const string screensTag = "Screen Bounds";
    private const string deathTag = "Death Zone";
    #endregion

    #region Component Accessors
    public Animator PlayerAnimator { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D RigidBody { get; private set; }
    public TrailRenderer TrailRenderer { get; private set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Components
        BoxCollider = GetComponent<BoxCollider2D>();
        RigidBody = GetComponent<Rigidbody2D>();
        //SpriteRenderer = this.GetComponent<SpriteRenderer>();
        PlayerAnimator = GetComponent<Animator>();
        TrailRenderer = GetComponent<TrailRenderer>();

        // Add all player states to list
        playerStates.AddRange(new List<PlayerState>
        {
            idleState,
            runState,
            jumpState,
            wallSlidingState,
            wallJumpState,
            fallState,
            dashState,
            deathState,
            respawnState
        });

        // Start idle
        ChangeToState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        // Let current state deal with inputs, state changes and animation
        currentState.HandleInput();
        currentState.LogicUpdate();
        currentState.SpriteUpdate();
    }

    // Physics calcs
    private void FixedUpdate()
    {
        // Current state performs movement
        currentState.PhysicsUpdate();
    }

    // Switch to the given state and disable others
    public void ChangeToState(PlayerState state)
    {
        foreach (PlayerState playerState in playerStates) {
            playerState.gameObject.SetActive(false);
        }

        state.gameObject.SetActive(true);
        currentState = state;
    }

    // If the player is grounded or not
    public bool IsGrounded()
    {
        // Raycast
        float heightBuffer = .05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(BoxCollider.bounds.center,
            BoxCollider.bounds.size,
            0f,
            Vector2.down,
            heightBuffer,
            groundLayer);

        return (raycastHit.collider != null);
    }

    // If the player is touching a wall left and is not grounded
    public bool TouchingWallRight()
    {
        if (IsGrounded())
        {
            return false;
        }

        float widthBuffer = .05f;

        // Raycast right
        RaycastHit2D raycastRight = Physics2D.BoxCast(BoxCollider.bounds.center,
            BoxCollider.bounds.size,
            0f,
            Vector2.right,
            widthBuffer,
            groundLayer);

        return (raycastRight.collider != null) ? true : false;

    }

    // If the player is touching a wall left and is not grounded
    public bool TouchingWallLeft()
    {
        if (IsGrounded())
        {
            return false;
        }

        float widthBuffer = .05f;
       
        // Raycast left
        RaycastHit2D raycastLeft = Physics2D.BoxCast(BoxCollider.bounds.center,
            BoxCollider.bounds.size,
            0f,
            Vector2.left,
            widthBuffer,
            groundLayer);

        return (raycastLeft.collider != null) ? true : false;
      
    }



    // If the player is aerial and falling towards the ground
    public bool IsFalling()
    {
        return (RigidBody.velocity.y < -0.05) && !IsGrounded();
    }


    // Removes a dash from the remaining total
    public void RemoveDash()
    {
        remainingDashes -= 1;
    }

    // Resets the dash count
    public void ResetDashes()
    {
        remainingDashes = 1;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Moving camera if colliding with new screen
        if (collision.gameObject.CompareTag(screensTag))
        {
            cameraController.UpdateCameraScreen(collision);
        }
        // Starting death/respawn process if colliding with a death zone
        else if (collision.gameObject.CompareTag(deathTag))
        {
            if (currentState != deathState)
            {
                ChangeToState(deathState);
            }
        }
        

    }

}


