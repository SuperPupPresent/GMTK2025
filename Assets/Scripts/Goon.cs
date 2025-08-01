using UnityEngine;
using System.Collections;

public class Goon : MonoBehaviour
{
    public bool playerInner;
    public bool playerOuter;

    public GameObject hitBox; //Attack from the goon
    public GameObject player;
    public Animator animator;

    [SerializeField] private float moveSpeed;

    private float attackTime = 2f;
    //private float stunnedTime = 2f;

    public GoonHealth health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
            animator.SetBool("isWalking", false);
        }

        //Attack state
        if (playerInner && attackTime > 0 && !health.stunned)
        {
            //Debug.Log("GOON ATTACK!");
            attackTime -= Time.deltaTime;
            if(attackTime <= 0)
            {
                StartCoroutine(attack());
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
        yield return new WaitForSeconds(.25f);
        hitBox.SetActive(false);
        yield return new WaitForSeconds(.5f);
        hitBox.SetActive(true);
        yield return new WaitForSeconds(.25f);
        hitBox.SetActive(false);
        attackTime = 2f;
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
