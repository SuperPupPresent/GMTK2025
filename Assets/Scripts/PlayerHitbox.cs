using UnityEngine;

//The attack sent out by the player, attaches to the attack hitbox
public class PlayerHitbox : MonoBehaviour
{
    GoonHealth goonHealth;
    [SerializeField] int attackDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            goonHealth = collision.gameObject.GetComponent<GoonHealth>();
            StartCoroutine(goonHealth.takeDamage(attackDamage));
            //Debug.Log("Hurt Goon");
        }
    }
}
