using UnityEngine;

public class GoonSpawner : MonoBehaviour
{

    BoxCollider2D hitbox;

    public CameraMovement cameraMovement;

    public GameObject goon1;
    public GameObject goon2;
    public GameObject goon3;
    public GameObject goon4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goon1.SetActive(false);
        goon2.SetActive(false);
        goon3.SetActive(false);
        goon4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player Entered Goon Spawner");
            // Spawn a goon here
            goon1.SetActive(true);
            goon2.SetActive(true);
            goon3.SetActive(true);
            goon4.SetActive(true);

            cameraMovement.lockHorizontal = true;

            Destroy(hitbox); // Destroy the spawner after spawning goons

            //if all goons are dead, unlock camera movement
            if (goon1.GetComponent<GoonHealth>().dead && goon2.GetComponent<GoonHealth>().dead && goon3.GetComponent<GoonHealth>().dead && goon4.GetComponent<GoonHealth>().dead)
            {
                cameraMovement.lockHorizontal = false;
            }

            // Optionally, you can set the parent of the goon to this spawner

        }
    }
}
