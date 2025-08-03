using UnityEngine;

public class BossArena : MonoBehaviour
{

    public CameraMovement cameraMovement;
    public GameObject boss;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            cameraMovement.lockHorizontal = true;
        }
    }
}
