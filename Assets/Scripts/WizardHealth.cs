using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizardHealth : MonoBehaviour
{
    [SerializeField] private float maxEnemyHealth;
    [HideInInspector] public float currentHealth;

    public bool dead;
    public Animator enemyAnimator;
    public Animator turtleAnimator;
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
        enemyAnimator.SetBool("isDead", true);

        audioSource.clip = deathSound;
        audioSource.volume = 0.8f;
        audioSource.Play(); // plays death sound

        yield return new WaitForSeconds(3f);
        turtleAnimator.SetBool("isSaved", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
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

