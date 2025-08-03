using UnityEngine;

public class RotationTrigger : MonoBehaviour
{
    public TimeWizzard script;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "WizzardTrigger")
        {
            Debug.Log("Triggered!");
            script.currentNumberOfRotations++;
        }
    }
}
