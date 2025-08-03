using UnityEngine;

public class TimeWizzard : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public Transform player;

    private int currentDash = 0;
    private bool checkingState = true;

    private Vector3 startingPosition;
    private Vector3 playerStartingPosition;
    private bool isDashing = false;

    [SerializeField] float dashSpeed = 10;

    [SerializeField] float moveSpeed, radius;
    float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        facePlayer();
        standardMovement();
        //dashAttack();
        Debug.Log(currentDash);
        //animator.SetBool("startDash", true);
    }


    void standardMovement()
    {
        angle += moveSpeed * Time.deltaTime;
        transform.position = new Vector3(1.5f* Mathf.Cos(angle), Mathf.Sin(-angle) -.5f, 0) * radius;
    }

    //TODO: Make it so the it dashes three times along with implement other attacks
    void dashAttack()
    {
        if(currentDash == 0)
        {
            animator.SetBool("startDash", true);
            startingPosition = transform.position;
            currentDash = 1;
        }
        

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("dashReset") && checkingState)
            {
                playerStartingPosition = player.position;
                checkingState = false;
                animator.SetBool("startDash", true);
                transform.position = startingPosition;
                currentDash++;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("continueDash") || animator.GetCurrentAnimatorStateInfo(0).IsName("dashStart"))
            {
                clockDash();
            }
            
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            currentDash = 0;
            animator.SetBool("finishDash", false);
        }
        
        if ((transform.position - startingPosition).magnitude > 20)
        {
            isDashing = false;
            checkingState = true;
            rb.linearVelocity = new Vector2(0, 0);

            if (currentDash >= 3)
            {
                animator.SetBool("finishDash", true);
            }
            else
            {
                //Debug.Log("reset");
                animator.SetBool("startDash", false);
            }
        }


    }

    void clockDash()
    {
        isDashing = true;
        rb.linearVelocity = dashSpeed * (playerStartingPosition - startingPosition);
        //transform.localEulerAngles = player.position - startingPosition;
    }

    void facePlayer()
    {
        if (!isDashing)
        {
            if (player.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
            
    }

}
