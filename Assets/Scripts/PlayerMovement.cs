using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset playerInputActions;

    public Animator animator;
    
    public Rigidbody2D PlayerRb;
    public Rigidbody2D ParentRb;

    public GameObject cameraObject;

    public PlayerAttack attackScript;
    public PlayerHealth health;

    private InputAction jumpButton;
    private InputAction moveButton;
    private Vector2 moveAmt;

    [SerializeField] int moveSpeed = 5;
    [SerializeField] float travelSpeed = .5f;
    [SerializeField] float maxHeight = 5f;

    private bool facingRight = true;
    private bool isJumping = false;
    private float jumpSpeed;
    private float cameraRadius;
    private Transform CameraTransform;

    //Vishnu Recall Stuff
    private Queue<(Vector3 position, float time)> positionHistory = new(); // position queue
    private float positionRecordDuration = 2.1f; // recall duration
    public GameObject playerContainer; // reference to container used for recall

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
        cameraRadius = cameraObject.GetComponent<Camera>().orthographicSize * cameraObject.GetComponent<Camera>().aspect;
        CameraTransform = cameraObject.GetComponent<Transform>();
        health = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        moveAmt = moveButton.ReadValue<Vector2>();

        movement(moveAmt);
        animator.SetBool("isRunning", moveAmt != new Vector2(0, 0));
        determineRotation();

        if (jumpButton.WasPressedThisFrame())
        {
            initiateJump();
        }

        validateJump();

        //record position with timestamp
        float now = Time.time;
        positionHistory.Enqueue((playerContainer.transform.position, now));
        //remove old positions
        while (positionHistory.Count > 0 && now - positionHistory.Peek().time > positionRecordDuration)
        {
            positionHistory.Dequeue();
        }
    }

    public List<Vector3> GetPositionsForRecall(float seconds)
    {
        List<Vector3> positions = new List<Vector3>();
        float now = Time.time;
        foreach (var entry in positionHistory)
        {
            if (now - entry.time <= seconds)
            {
                positions.Add(entry.position);
            }
        }
        return positions;
    }

    private void movement(Vector2 moveAmt)
    {
        // Restricts movement when:
        if (!attackScript.isAttacking && !health.stunned && !attackScript.isBlocking)
        {
            animator.SetBool("isRunning", true);
            //Top of screen
            if (ParentRb.transform.position.y >= 2)
            {
                ParentRb.linearVelocityY = Mathf.Clamp(moveAmt.y * moveSpeed, -moveSpeed, 0);
                //ParentRb.linearVelocityX = (moveAmt.x * moveSpeed);

            }
            //Botton of screen
            else if (ParentRb.transform.position.y <= -6.5)
            {
                ParentRb.linearVelocityY = Mathf.Clamp(moveAmt.y * moveSpeed, 0, moveSpeed);
                //ParentRb.linearVelocityX = (moveAmt.x * moveSpeed);
            }
            else
            {
                ParentRb.linearVelocityY = moveAmt.y * moveSpeed;
            }
            //Right of screen
            if (ParentRb.transform.position.x >= CameraTransform.position.x + cameraRadius)
            {
                ParentRb.linearVelocityX = Mathf.Clamp(moveAmt.x * moveSpeed, -moveSpeed, 0);
            }
            //Left of screen
            else if (ParentRb.transform.position.x <= CameraTransform.position.x - cameraRadius)
            {
                //ParentRb.linearVelocityY = moveAmt.y * moveSpeed;
                ParentRb.linearVelocityX = Mathf.Clamp(moveAmt.x * moveSpeed, 0, moveSpeed);
            }
            else
            {
                ParentRb.linearVelocityX = moveAmt.x * moveSpeed;
            }
        }
        else
        {
            ParentRb.linearVelocity = new Vector2(0,0);
            animator.SetBool("isRunning", false);
        }
            
    }

    private void determineRotation()
    {
        if (!attackScript.isAttacking && !health.stunned && !attackScript.isBlocking)
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
    }

    private void initiateJump()
    {
        if (!isJumping)
        {
            isJumping = true;
            jumpSpeed = -maxHeight;
            animator.SetBool("isJumping", true);
        }
    }

    private void validateJump()
    {
        if (isJumping)
        {
            // Debug.Log("SHEVA");
            PlayerRb.transform.localPosition = new Vector3(0, (Mathf.Pow(maxHeight, 2f) - Mathf.Pow(jumpSpeed, 2f)), 0);
            jumpSpeed += travelSpeed;
            //Debug.Log(jumpSpeed);

            if (jumpSpeed >= 0)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                //Debug.Log("POW");
            }

            if (PlayerRb.transform.localPosition.y <= -0.1)
            {
                animator.SetBool("isFalling", false);
                isJumping = false;
                jumpSpeed = 0;
                PlayerRb.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
