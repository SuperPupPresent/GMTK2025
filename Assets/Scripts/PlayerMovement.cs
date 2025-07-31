using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset playerInputActions;
    public Rigidbody2D rb;

    private InputAction jump;
    private InputAction move;
    private Vector2 moveAmt;

    [SerializeField] int moveSpeed = 5;

    private bool facingRight = true;

    private void OnEnable()
    {
        playerInputActions.FindActionMap("PlayerAttack").Enable();
    }

    private void OnDisable()
    {
        playerInputActions.FindActionMap("PlayerAttack").Disable();
    }

    private void Awake()
    {
        jump = InputSystem.actions.FindAction("jump");
        move = InputSystem.actions.FindAction("Move");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveAmt = move.ReadValue<Vector2>();
        rb.linearVelocity = (moveAmt * moveSpeed);

        if(moveAmt.x > 0)
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if(moveAmt.x < 0)
        {
            rb.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (jump.WasPressedThisFrame())
        {
            Debug.Log("JUMP!");
        }
    }
}
