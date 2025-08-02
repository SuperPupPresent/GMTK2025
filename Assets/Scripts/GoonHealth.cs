using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GoonHealth : MonoBehaviour
{
    [SerializeField] private float maxEnemyHealth;
    [HideInInspector] public float currentHealth;
    public Animator enemyAnimator;
    public bool stunned;
    public bool dead;
    public float stunnedTime;
    public bool facingRight;
    float fallBackTime;
    public GameObject hitbox; //Attack from the goon

    float damageBuildup;
    public float recoverTimeMultiplier;
    public float knockBackLimit;
    Rigidbody2D rb;


    //for sfx
    private AudioSource audioSource;
    public AudioClip deathSound;

    void Start()
    {
        facingRight = false;
        currentHealth = maxEnemyHealth;
        stunned = false;
        dead = false;
        fallBackTime = 0.5f;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Enemy slowly recovers
        if (damageBuildup > 0)
        {
            damageBuildup -= Time.deltaTime * recoverTimeMultiplier;
        }
    }

    public IEnumerator applyDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            hitbox.SetActive(false); // Disable hitbox when dead
            StartCoroutine(Dead());
        }
        else if (!dead)
        {
            damageBuildup += damage;
            if(damageBuildup >= knockBackLimit)
            {
                stunned = true;
                Debug.Log("KnockedBack!!");
                damageBuildup = 0;
                rb.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                yield return new WaitForSeconds(fallBackTime);
                Debug.Log("Reset Velocity");
                rb.linearVelocity = Vector2.zero;
                stunned = false;
            }
            else
            {
                stunned = true;
                enemyAnimator.Play("Hurt");
                yield return new WaitForSeconds(stunnedTime);
                stunned = false;
                enemyAnimator.Play("Idle");
            }
        }
        yield return null;
    }

    public IEnumerator Dead()
    {
        dead = true;
        enemyAnimator.Play("Dead");

        audioSource.clip = deathSound;
        audioSource.volume = 0.8f;
        audioSource.Play(); // plays death sound

        yield return new WaitForSeconds(stunnedTime);
        Time.timeScale = 1f;
    }

    public void Knockback()
    {
        Debug.Log("KnockedBack!!");
        damageBuildup = 0;
        if(!facingRight)
        {
            rb.AddForce(new Vector2(50, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-50, 0), ForceMode2D.Impulse);
        }
    }

    public void takeDamage(int damage)
    {
        StartCoroutine(applyDamage(damage));
    }
}
