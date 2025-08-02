using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameManager gameManager;
    public Animator playerAnimator;
    public PlayerAttack playerAttack;
    public bool stunned;
    private bool isDead = false;
    private float stunnedTime = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    //Audio Stuff
    private AudioSource audioSource;
    public AudioClip deathSound;

    void Start()
    {
        stunned = false;
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator takeDamage(int damage)
    {
        if (!playerAttack.isImmune)
        {
            if (playerAttack.isBlocking)
            {
                damage = damage/2;
            }
            gameManager.setHealth(-damage);
            if (gameManager.currentHealth <= 0 && !isDead)
            {
                playerAnimator.SetBool("isBlock", false);
                StartCoroutine(playerDead());
                yield return new WaitForEndOfFrame();
            }
            else if (!isDead)
            {
                stunned = true;
                playerAnimator.Play("Hurt");
                yield return new WaitForSeconds(stunnedTime);
                stunned = false;
                playerAnimator.Play("Idle");
            }
        }

        
    }

    public IEnumerator playerDead()
    {
        isDead = true;
        stunned = true;
        Time.timeScale = 0.5f;

        audioSource.clip = deathSound;
        audioSource.Play(); //plays death sound

        playerAnimator.Play("Dead");
        yield return new WaitForSeconds(stunnedTime);
        Time.timeScale = 1f;
    }
}
