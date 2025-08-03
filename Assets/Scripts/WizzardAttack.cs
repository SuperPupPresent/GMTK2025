using UnityEngine;

public class WizzardAttack : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] int attackDamage;

    public TimeWizzard script;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && script.isHostile)
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.takeDamage(attackDamage);
            //Debug.Log("Hurt Player");
        }
    }
}
