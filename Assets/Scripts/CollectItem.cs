using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heart")
        {
            gameManager.setHealth(20);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Mana")
        {
            gameManager.setMana(20);
            Destroy(collision.gameObject);
        }
    }
}
