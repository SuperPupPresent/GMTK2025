using System.Collections;
using UnityEngine;

public class GoonHealth : MonoBehaviour
{
    [SerializeField] private float maxEnemyHealth;
    [HideInInspector] public float currentHealth;
    public Animator enemyAnimator;
    public bool stunned;
    public bool dead;
    public float stunnedTime;
    public GameObject hitbox; //Attack from the goon

    float damageBuildup;
    public float recoverTimeMultiplier;
    public float knockBackLimit;


    //for sfx
    private AudioSource audioSource;
    public AudioClip deathSound;

    void Start()
    {
        currentHealth = maxEnemyHealth;
        stunned = false;
        dead = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Enemy slowly recovers
        if (damageBuildup > 0)
        {
            damageBuildup -= Time.deltaTime * recoverTimeMultiplier;
        }
    }

    public IEnumerator takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            hitbox.SetActive(false); // Disable hitbox when dead
            StartCoroutine(Dead());
            yield return new WaitForEndOfFrame();
        }
        else if (!dead)
        {
            damageBuildup += damage;
            if(damageBuildup >= knockBackLimit)
            {
                Knockback();
            }
            stunned = true;
            enemyAnimator.Play("Hurt");
            yield return new WaitForSeconds(stunnedTime);
            stunned = false;
            enemyAnimator.Play("Idle");
            Debug.Log("Idling");
        }
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

        damageBuildup = 0;
    }
}
