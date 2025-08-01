using UnityEngine;
using System.Collections;

public class Goon : MonoBehaviour
{
    public bool playerInner;
    public bool playerOuter;
    public bool isStunned;

    public GameObject hitBox;
    public GameObject player;

    [SerializeField] private float moveSpeed;

    private float attackTime = 2f;
    private float stunnedTime = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
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
        if(playerOuter && !playerInner && !isStunned)
        {
           transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
        }

        //Attack state
        if (playerInner && attackTime > 0 && !isStunned)
        {
            //Debug.Log("GOON ATTACK!");
            attackTime -= Time.deltaTime;
            if(attackTime <= 0)
            {
                StartCoroutine(attack());
            }
        }

        //Stunned State
        if (isStunned)
        {
            stunnedTime -= Time.deltaTime;
            if(stunnedTime <= 0)
            {
                isStunned = false;
                stunnedTime = 2f;
            }
        }
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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            transform.position = new Vector3(0, 0, 0);
            isStunned = true;
        }
    }
}
