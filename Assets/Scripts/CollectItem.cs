using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    //public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heart")
        {
            Destroy(collision.gameObject);
        }
    }
}
