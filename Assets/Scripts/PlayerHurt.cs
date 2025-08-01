using System.Collections;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    public Animator playerAnimator;
    public bool stunned;
    [SerializeField] float stunnedTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stunned = false;
    }

    public IEnumerator playerHurt()
    {
        playerAnimator.Play("Hurt");
        stunned = true;
        yield return new WaitForSeconds(stunnedTime);
        stunned = false;  
    }

    public void playerDead()
    {
        playerAnimator.Play("Dead");
        stunned = true;
    }
}
