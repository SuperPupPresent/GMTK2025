using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset playerInputActions;
    public Rigidbody2D PlayerRb;
    public Rigidbody2D ParentRb;

    private InputAction jumpButton;
    private InputAction moveButton;
    private Vector2 moveAmt;

    [SerializeField] int moveSpeed = 5;
    [SerializeField] float travelSpeed = .5f;
    [SerializeField] float maxHeight = 5f;

    private bool facingRight = true;
    private bool isJumping = false;
    private float jumpSpeed;

    private void OnEnable()
    {
        playerInputActions.FindActionMap("PlayerMovement").Enable();
    }

    private void OnDisable()
    {
        playerInputActions.FindActionMap("PlayerMovement").Disable();
    }

    private void Awake()
    {
        jumpButton = InputSystem.actions.FindAction("jump");
        moveButton = InputSystem.actions.FindAction("Move");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveAmt = moveButton.ReadValue<Vector2>();

        movement(moveAmt);
        determineRotation();

        if (jumpButton.WasPressedThisFrame())
        {
            initiateJump();
        }

        validateJump();
    }

    private void movement(Vector2 moveAmt)
    {
        if (ParentRb.transform.position.y >= 2)
        {
            ParentRb.linearVelocityY = Mathf.Clamp(moveAmt.y * moveSpeed, -moveSpeed, 0);
            ParentRb.linearVelocityX = (moveAmt.x * moveSpeed);
        }
        else if (ParentRb.transform.position.y <= -6.5)
        {
            ParentRb.linearVelocityY = Mathf.Clamp(moveAmt.y * moveSpeed, 0, moveSpeed);
            ParentRb.linearVelocityX = (moveAmt.x * moveSpeed);
        }
        else
        {
            ParentRb.linearVelocity = (moveAmt * moveSpeed);
        }
    }

    private void determineRotation()
    {
        if (moveAmt.x > 0)
        {
            PlayerRb.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (moveAmt.x < 0)
        {
            PlayerRb.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void initiateJump()
    {
        if (!isJumping)
        {
            isJumping = true;
            jumpSpeed = -maxHeight;
        }
    }

    private void validateJump()
    {
        if (isJumping)
        {
            // Debug.Log("SHEVA");
            PlayerRb.transform.localPosition = new Vector3(0, (Mathf.Pow(maxHeight, 2f) - Mathf.Pow(jumpSpeed, 2f)), 0);
            jumpSpeed += travelSpeed;

            if (PlayerRb.transform.localPosition.y <= -0.1)
            {
                isJumping = false;
                jumpSpeed = 0;
                PlayerRb.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
