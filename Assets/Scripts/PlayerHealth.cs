using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameManager gameManager;
    public Animator playerAnimator;
    public bool stunned;
    [SerializeField] float stunnedTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stunned = false;
    }

    public IEnumerator takeDamage(int damage)
    {
        gameManager.setHealth(-damage);
        if (gameManager.currentHealth <= 0)
        {
            StartCoroutine(playerDead());
            yield return new WaitForEndOfFrame();
        }
        else
        {
            stunned = true;
            playerAnimator.Play("Hurt");
            yield return new WaitForSeconds(stunnedTime);
            stunned = false;
            playerAnimator.Play("Idle");
        }
    }

    public IEnumerator playerDead()
    {
        stunned = true;
        Time.timeScale = 0.5f;
        playerAnimator.Play("Dead");
        yield return new WaitForSeconds(stunnedTime);
        Time.timeScale = 1f;
    }
}
