using UnityEngine;

public class GoonOuterTrigger : MonoBehaviour
{
    public Goon goonScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            goonScript.playerOuter = true;
            goonScript.player = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            goonScript.playerOuter = false;
            goonScript.player = null;
        }
    }
}
