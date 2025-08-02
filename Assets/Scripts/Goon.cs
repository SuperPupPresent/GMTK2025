using UnityEngine;
using System.Collections;
using UnityEngine.U2D;

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

    // for sfx
    private AudioSource audioSource;
    public AudioClip attackSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitBox.SetActive(false);
        audioSource = GetComponent<AudioSource>();
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
                health.facingRight = true;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
                health.facingRight = false;
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
    }

    IEnumerator attack()
    {
        audioSource.clip = attackSound;
        audioSource.volume = 0.8f;


        hitBox.SetActive(true);
        animator.Play("Punch One");
        float pitch = Random.Range(0.9f, 1.1f);
        audioSource.pitch = pitch;
        audioSource.Play(); // plays attack sound
        yield return new WaitForSeconds(.25f);
        hitBox.SetActive(false);

        yield return new WaitForSeconds(.5f);

        hitBox.SetActive(true);
        animator.Play("Punch Two");
        pitch = Random.Range(0.9f, 1.1f);
        audioSource.pitch = pitch;
        audioSource.Play();
        yield return new WaitForSeconds(.25f);
        hitBox.SetActive(false);

        timeBeforeAttack = 1.5f;
    }
}