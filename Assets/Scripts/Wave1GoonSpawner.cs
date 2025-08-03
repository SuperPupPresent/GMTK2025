using System.Collections;
using UnityEngine;

public class Wave1GoonSpawner : MonoBehaviour
{

    public CameraMovement cameraMovement;

    public GameObject goon1;
    public GameObject goon2;
    public GameObject goon3;
    public GameObject goon4;


    public bool wave1 = false;
    public bool wave2 = false;
    public bool wave3 = false;

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

        //Wave 1: 4 guys
        //Wave 2: 6 guys
        //Wave 3: 6 guys

        if (wave1)
        {
            goon1.SetActive(true);
            goon2.SetActive(true);
            goon3.SetActive(true);
            goon4.SetActive(true);

            goon1.GetComponent<GoonHealth>().fromSpawner = true;
            goon2.GetComponent<GoonHealth>().fromSpawner = true;
            goon3.GetComponent<GoonHealth>().fromSpawner = true;
            goon4.GetComponent<GoonHealth>().fromSpawner = true;

        }


        if (goon1.GetComponent<GoonHealth>().dead && goon2.GetComponent<GoonHealth>().dead && goon3.GetComponent<GoonHealth>().dead && goon4.GetComponent<GoonHealth>().dead)
        {
            cameraMovement.lockHorizontal = false; // Unlock camera movement if all wave 1 goons are dead and there are no other waves
            StartCoroutine(WaitABit(gameObject, 3f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if goon 1-4 are not null wave 1 exists
            if (goon1 != null && goon2 != null && goon3 != null && goon4 != null) { wave1 = true; }

            cameraMovement.lockHorizontal = true;
        }
    }

    private IEnumerator WaitABit(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(target); // Destroy the spawner after a short delay
    }
   
}
