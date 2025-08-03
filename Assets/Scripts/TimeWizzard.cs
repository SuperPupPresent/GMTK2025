using UnityEngine;

public class TimeWizzard : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public Transform player;
    public GameObject clockProjectile;
    public Transform timeAreana;

    public bool isHostile;

    private int currentDash = 0;
    private bool checkingState = true;
    private bool finishedAttack = false;

    private Vector3 startingPosition;
    private Vector3 playerStartingPosition;
    private bool isDashing = false;
    
    private int clocksThrown;
    private bool isThrown;
    private bool isBegining = true;

    private int desiredNumberOfRotations;
    public int currentNumberOfRotations;

    private int currentAttack;

    private bool isEnvelope;

    [SerializeField] float dashSpeed = 10;
    [SerializeField] float projSpeed = 5;

    [SerializeField] float moveSpeed, radius;
    float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        desiredNumberOfRotations = Random.Range(4,6);

    }

    // Update is called once per frame
    void Update()
    {
        facePlayer();
        
        if (currentNumberOfRotations >= desiredNumberOfRotations)
        {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && finishedAttack)
            {
                currentNumberOfRotations = 0;
                finishedAttack = false;
                isBegining = true;
                if(currentAttack == 2)
                
                {
                    currentAttack = 0;
                }
                else
                {
                    currentAttack++;
                }
                animator.SetBool("finishDash", false);
                animator.SetBool("FinishThrow", false);

            }
            else
            {
                // Debug.Log(currentAttack);
                isHostile = true;
                if(currentAttack == 0)
                {
                    clockThrow();
                }
                
                else if(currentAttack == 1)
                {
                    dashAttack();
                }

                else if(currentAttack == 2)
                {
                    envelope();
                }
            }
        }
        else
        {
            isHostile = false;
            standardMovement();
        }

        //
        //Debug.Log(currentNumberOfRotations);
        //animator.SetBool("startDash", true);
    }


    void standardMovement()
    {
        angle += moveSpeed * Time.deltaTime;
        transform.position = new Vector3((1.5f* Mathf.Cos(angle)) + 20f, Mathf.Sin(-angle) -.5f, 0) * radius;
    }

    //TODO: Make it so the it dashes three times along with implement other attacks
    void dashAttack()
    {
        
        if(currentDash == 0 && isBegining)
        {
            animator.SetBool("startDash", true);
            startingPosition = transform.position;
            currentDash = 1;
            isBegining = false;
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

            if (currentDash >= 2)
            {
                animator.SetBool("finishDash", true);
                animator.SetBool("startDash", false);
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
        //Debug.Log(clocksThrown);
        if(clocksThrown == 0 && !isThrown && isBegining)
        {
            animator.SetBool("clockThrow", true);
            isThrown = true;
            isBegining = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("clockThrow") && isThrown)
            {
                Debug.Log("THROWING CLOCK");
                animator.SetBool("clockThrow", false);
                clocksThrown++;
                var proj = Instantiate(clockProjectile);

                proj.transform.position = new Vector3(transform.position.x, transform.position.y + 9, transform.position.z);
                proj.SetActive(true);
                Vector3 playerPos = player.transform.position;
                Vector3 projPos = proj.transform.position;

                Vector2 launchSpeed = new Vector2(projPos.x - playerPos.x, projPos.y - playerPos.y);
                launchSpeed.Normalize();
                proj.GetComponent<Rigidbody2D>().linearVelocity = -projSpeed * launchSpeed;
                
                isThrown = false;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("clockThrowIdle") && !animator.GetBool("FinishThrow"))
            {
                
                if(clocksThrown >= 3)
                {
                    animator.SetBool("FinishThrow", true);
                    animator.SetBool("clockThrow", false);
                    finishedAttack = true;
                    clocksThrown = 0;
                    isThrown = false;
                }

                else
                {
                    animator.SetBool("clockThrow", true);
                    isThrown = true;
                }
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                clocksThrown = 0;
                isThrown = false;
                isBegining = true;
                animator.SetBool("FinishThrow", false);
            }

        }
    }

    void envelope()
    {
        if (isBegining)
        {
            isBegining = false;
            animator.SetBool("startEnvelope", true);
            animator.SetBool("endEnvelope", false);
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 4);
            isEnvelope = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("startEnvelope"))
            {
                animator.SetBool("startEnvelope", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("envelopeAttack"))
            {
                animator.SetBool("endEnvelope", true);
                Debug.Log("in attack");
                finishedAttack = true;
                isEnvelope = false;
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
        if (!isDashing || isEnvelope)
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
