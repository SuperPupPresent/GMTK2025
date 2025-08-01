using UnityEngine;

//The attack sent out by the goons, attaches to the attack hitbox
public class GoonAttack : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] int attackDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            StartCoroutine(playerHealth.takeDamage(attackDamage));
            //Debug.Log("Hurt Player");
        }
    }
}
