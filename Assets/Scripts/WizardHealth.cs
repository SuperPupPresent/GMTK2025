using System.Collections;
using UnityEngine;

public class WizardHealth : MonoBehaviour
{
    [SerializeField] private float maxEnemyHealth;
    [HideInInspector] public float currentHealth;

    public bool dead;
    Animator animator;
    private AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip hurtSound;

    void Start()
    {
        currentHealth = maxEnemyHealth;
        dead = false;
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator Dead()
    {
        dead = true;
        animator.SetBool("isDead", true);

        audioSource.clip = deathSound;
        audioSource.volume = 0.8f;
        audioSource.Play(); // plays death sound

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            StartCoroutine(Dead());
        }
        else if (!dead)
        {
            audioSource.clip = hurtSound;
            //audioSource.volume = 0.8f;
            audioSource.Play(); 
        }
    }
}

