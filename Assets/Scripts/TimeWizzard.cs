using UnityEngine;

public class TimeWizzard : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    private int currentDash = 0;
    private bool checkingState = true;

    private Vector3 startingPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dataAttack();
        Debug.Log(currentDash);
        //animator.SetBool("startDash", true);
    }


    //TODO: Make it so the it dashes three times along with implement other attacks
    void dataAttack()
    {
        if(currentDash == 0)
        {
            animator.SetBool("startDash", true);
            startingPosition = transform.position;
            currentDash = 1;
        }
        

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("dashReset"))
            {
                animator.SetBool("startDash", true);
                transform.position = startingPosition;
                currentDash++;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("continueDash") || animator.GetCurrentAnimatorStateInfo(0).IsName("dashStart"))
            {
                clockDash();
            }

            checkingState = false;
        }


    }

    void clockDash()
    {
        rb.linearVelocity = new Vector2(5, 0);
        if(transform.position.x > startingPosition.x + 10)
        {
            rb.linearVelocity = new Vector2(0, 0);
            
            if(currentDash >= 3)
            {
                animator.SetBool("finishDash", true);
                currentDash = -1;
            }
            else
            {
                animator.SetBool("startDash", false);
            }
        }
    }

    void clockDashReset()
    {

    }
}
