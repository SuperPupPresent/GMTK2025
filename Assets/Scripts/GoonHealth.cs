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
    public bool fromSpawner = false; // If true, the goon is spawned by the spawner script
    public GameObject hitbox; //Attack from the goon

    float damageBuildup;
    public float recoverTimeMultiplier;
    public float knockBackLimit;
    float knockBackPower;
    public float knockBackTime;
    bool knockedBack = false;
    Rigidbody2D rb;


    //for sfx
    private AudioSource audioSource;
    public AudioClip deathSound;

    void Start()
    {
        knockBackPower = 15;
        facingRight = false;
        currentHealth = maxEnemyHealth;
        stunned = false;
        dead = false;
        fallBackTime = 0.5f;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        knockBackTime = 0.5f;
    }

    private void Update()
    {
        //Enemy slowly recovers
        if (damageBuildup > 0)
        {
            damageBuildup -= Time.deltaTime * recoverTimeMultiplier;
        }
        if(damageBuildup >= knockBackLimit)
        {
            Knockback();
            
        }
        if (knockedBack)
        {
            knockBackTime -= Time.deltaTime;
            if(knockBackTime <= 0)
            {
                knockedBack = false;
                knockBackTime = 0.5f;
                rb.linearVelocity = Vector2.zero;
                enemyAnimator.SetBool("isKnocked", false);
            }
        }
    }

    IEnumerator applyDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            hitbox.SetActive(false); // Disable hitbox when dead
            StartCoroutine(Dead());
        }
        else if (!dead && !knockedBack)
        {
            damageBuildup += damage;
            stunned = true;
            enemyAnimator.Play("Hurt");
            yield return new WaitForSeconds(stunnedTime);
            stunned = false;
            enemyAnimator.Play("Idle");
        }
        yield return null;
    }

    public IEnumerator Dead()
    {
        dead = true;
        enemyAnimator.SetBool("isDead", true);

        audioSource.clip = deathSound;
        audioSource.volume = 0.8f;
        audioSource.Play(); // plays death sound

        yield return new WaitForSeconds(3f);
        if (!fromSpawner)
        {
            Destroy(gameObject);
        }


    }

    void Knockback()
    {
        knockedBack = true;
        damageBuildup = 0;
        enemyAnimator.SetBool("isKnocked", true);
        if(!facingRight)
        {
            rb.AddForce(new Vector2(knockBackPower, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-knockBackPower, 0), ForceMode2D.Impulse);
        }
    }

    public void takeDamage(int damage)
    {
        StartCoroutine(applyDamage(damage));
    }
}
