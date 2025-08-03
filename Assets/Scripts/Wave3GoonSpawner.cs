using System.Collections;
using UnityEngine;

public class Wave3GoonSpawner : MonoBehaviour
{

    public CameraMovement cameraMovement;

    public GameObject goon1;
    public GameObject goon2;
    public GameObject goon3;
    public GameObject goon4;
    public GameObject goon5;
    public GameObject goon6;
    public GameObject goon7;
    public GameObject goon8;
    public GameObject goon9;
    public GameObject goon10;
    public GameObject goon11;
    public GameObject goon12;
    public GameObject goon13;
    public GameObject goon14;
    public GameObject goon15;
    public GameObject goon16;

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
        goon5.SetActive(false);
        goon6.SetActive(false);
        goon7.SetActive(false);
        goon8.SetActive(false);
        goon9.SetActive(false);
        goon10.SetActive(false);
        goon11.SetActive(false);
        goon12.SetActive(false);
        goon13.SetActive(false);
        goon14.SetActive(false);
        goon15.SetActive(false);
        goon16.SetActive(false);

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
        //if there is a wave 2 and all enemies in wave 1 are dead, activate wave 2
        if (wave2 && goon1.GetComponent<GoonHealth>().dead && goon2.GetComponent<GoonHealth>().dead && goon3.GetComponent<GoonHealth>().dead && goon4.GetComponent<GoonHealth>().dead)
        {
            goon5.SetActive(true);
            goon6.SetActive(true);
            goon7.SetActive(true);
            goon8.SetActive(true);
            goon9.SetActive(true);
            goon10.SetActive(true);

            goon5.GetComponent<GoonHealth>().fromSpawner = true;
            goon6.GetComponent<GoonHealth>().fromSpawner = true;
            goon7.GetComponent<GoonHealth>().fromSpawner = true;
            goon8.GetComponent<GoonHealth>().fromSpawner = true;
            goon9.GetComponent<GoonHealth>().fromSpawner = true;
            goon10.GetComponent<GoonHealth>().fromSpawner = true;
        }
        //if there is a wave 3 and all enemies in wave 2 are dead, activate wave 3
        if (wave3 && goon5.GetComponent<GoonHealth>().dead && goon6.GetComponent<GoonHealth>().dead && goon7.GetComponent<GoonHealth>().dead && goon8.GetComponent<GoonHealth>().dead && goon9.GetComponent<GoonHealth>().dead && goon10.GetComponent<GoonHealth>().dead)
        {
            goon11.SetActive(true);
            goon12.SetActive(true);
            goon13.SetActive(true);
            goon14.SetActive(true);
            goon15.SetActive(true);
            goon16.SetActive(true);

            goon11.GetComponent<GoonHealth>().fromSpawner = true;
            goon12.GetComponent<GoonHealth>().fromSpawner = true;
            goon13.GetComponent<GoonHealth>().fromSpawner = true;
            goon14.GetComponent<GoonHealth>().fromSpawner = true;
            goon15.GetComponent<GoonHealth>().fromSpawner = true;
            goon16.GetComponent<GoonHealth>().fromSpawner = true;
        }

        if (goon11.GetComponent<GoonHealth>().dead && goon12.GetComponent<GoonHealth>().dead && goon13.GetComponent<GoonHealth>().dead && goon14.GetComponent<GoonHealth>().dead && goon15.GetComponent<GoonHealth>().dead && goon16.GetComponent<GoonHealth>().dead)
        {
            cameraMovement.lockHorizontal = false; // Unlock camera movement if all wave 3 goons are dead
            StartCoroutine(WaitABit(gameObject, 3f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if goon 1-4 are not null wave 1 exists
            if (goon1 != null && goon2 != null && goon3 != null && goon4 != null) { wave1 = true; }

            // if goon 5-10 are not null wave 2 exits
            if (goon5 != null && goon6 != null && goon7 != null && goon8 != null && goon9 != null && goon10 != null) { wave2 = true; }

            // if goon 11-16 are not null wave 3 exists
            if (goon11 != null && goon12 != null && goon13 != null && goon14 != null && goon15 != null && goon16 != null) { wave3 = true; }

            cameraMovement.lockHorizontal = true;
        }
    }

    private IEnumerator WaitABit(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(target); // Destroy the spawner after a short delay
    }
   
}
