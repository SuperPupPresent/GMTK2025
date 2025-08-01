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

    void Start()
    {
        currentHealth = maxEnemyHealth;
        stunned = false;
        dead = false;
    }

    public IEnumerator takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(Dead());
            yield return new WaitForEndOfFrame();
        }
        else
        {
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
        yield return new WaitForSeconds(stunnedTime);
        Time.timeScale = 1f;
    }
}
