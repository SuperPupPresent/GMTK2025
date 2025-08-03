using UnityEngine;

public class TimeWizzard : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public Transform player;
    public GameObject clockProjectile;

    private int currentDash = 0;
    private bool checkingState = true;
    private bool finishedAttack = false;

    private Vector3 startingPosition;
    private Vector3 playerStartingPosition;
    private bool isDashing = false;
    
    private int clocksThrown;
    private bool isThrown;

    private int desiredNumberOfRotations;
    public int currentNumberOfRotations = 10;

    [SerializeField] float dashSpeed = 10;

    [SerializeField] float moveSpeed, radius;
    float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        desiredNumberOfRotations = Random.Range(4,5);

    }

    // Update is called once per frame
    void Update()
    {
        facePlayer();
        
        if (currentNumberOfRotations >= desiredNumberOfRotations)
        {
            //dashAttack();
            clockThrow();

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && finishedAttack)
            {
                currentNumberOfRotations = 0;
                finishedAttack = false;
            }
        }
        else
        {
            standardMovement();
        }

        //
        Debug.Log(currentNumberOfRotations);
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
                finishedAttack = true;
            }
            else
            {
                //Debug.Log("reset");
                animator.SetBool("startDash", false);
            }
        }

    }

    void clockThrow()
    {
        Debug.Log(clocksThrown);
        if(clocksThrown == 0)
        {
            animator.SetBool("clockThrow", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("clockThrow"))
            {
                animator.SetBool("clockThrow", false);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("clockThrowIdle") && !animator.GetBool("FinishThrow"))
            {
                clocksThrown++;
                var proj = Instantiate(clockProjectile);
                proj.SetActive(true);
                animator.SetBool("clockThrow", true);

                if(clocksThrown >= 3)
                {
                    animator.SetBool("FinishThrow", true);
                }
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
