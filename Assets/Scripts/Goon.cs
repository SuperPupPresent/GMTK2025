using UnityEngine;
using System.Collections;

public class Goon : MonoBehaviour
{
    public bool playerInner;
    public bool playerOuter;

    public GameObject hitBox; //Attack from the goon
    [HideInInspector] public GameObject player;
    public Animator animator;

    [SerializeField] private float moveSpeed;

    private float timeBeforeAttack = 1f;
    float stunTime = 0;
    //bool idling = false;

    public GoonHealth health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitBox.SetActive(false);
    }

    void FixedUpdate()
    {
        if (health.dead)
        {
            StopAllCoroutines();
            return;
        }
        //Changes rotation
        if(player != null && health.currentHealth > 0)
        {
            if(player.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }

        //The move towards player state
        if(playerOuter && !playerInner && !health.stunned)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
        }
        else
        {
            //Debug.Log("Stopped " + playerOuter + " " + !playerInner + " " + !health.stunned);
            animator.SetBool("isWalking", false);
        }

        //Attack state
        if (playerInner && timeBeforeAttack > 0 && !health.stunned)
        {
            //Debug.Log("GOON ATTACK!");
            timeBeforeAttack -= Time.deltaTime;
            if(timeBeforeAttack <= 0)
            {
                StartCoroutine(attack());
            }
        }

        if (health.stunned)
        {
            stunTime += Time.deltaTime;
            if (stunTime > 0.2)
            {
                health.stunned = false;
                stunTime = 0;
            }
        }
        ////Stunned State
        //if (isStunned)
        //{
        //    stunnedTime -= Time.deltaTime;
        //    if(stunnedTime <= 0)
        //    {
        //        isStunned = false;
        //        stunnedTime = 2f;
        //    }
        //}
    }

    IEnumerator attack()
    {
        hitBox.SetActive(true);
        animator.Play("Punch One");
        yield return new WaitForSeconds(.25f);
        hitBox.SetActive(false);
        yield return new WaitForSeconds(.5f);
        hitBox.SetActive(true);
        animator.Play("Punch Two");
        yield return new WaitForSeconds(.25f);
        hitBox.SetActive(false);
        timeBeforeAttack = 1.5f;
    }



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Hitbox")
    //    {
    //        transform.position = new Vector3(0, 0, 0);
    //        isStunned = true;
    //    }
    //}
}
