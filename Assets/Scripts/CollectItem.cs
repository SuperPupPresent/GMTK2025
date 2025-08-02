using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public GameManager gameManager;

    private AudioSource audioSource;
    public AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Heart")
        {
            gameManager.setHealth(20);
            Destroy(collision.gameObject);
            audioSource.clip = collectSound;
            audioSource.Play(); // plays collect sound
        }
        else if (collision.gameObject.tag == "Mana")
        {
            gameManager.setMana(20);
            Destroy(collision.gameObject);
            audioSource.clip = collectSound;
            audioSource.Play(); // plays collect sound
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
