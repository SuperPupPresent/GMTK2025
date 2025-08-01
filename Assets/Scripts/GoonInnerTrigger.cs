using UnityEngine;

public class GoonInnerTrigger : MonoBehaviour
{
    public Goon goonScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            goonScript.playerInner = true;
        }

        Debug.Log("INNER: " + collision.tag);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            goonScript.playerInner = false;
            Debug.Log("Player Left attack range");
        }
    }
}
