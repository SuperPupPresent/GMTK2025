using UnityEngine;

public class BossArena : MonoBehaviour
{

    public CameraMovement cameraMovement;
    public GameObject boss;
    public StarMovement stars1;
    public StarMovement stars2;
    public GameObject healthBar;

    public bool bossSpawned = false;

    private AudioSource audioSource;
    public AudioClip bossMusic;
    public AudioClip baseMusic;
    PlayerMovement playerMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.SetActive(false);
        boss.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        if(bossSpawned)
        {
            if (!audioSource.isPlaying || audioSource.clip != bossMusic)
            {
                audioSource.Stop();
                audioSource.clip = bossMusic;
                audioSource.Play();
            }
        }
        else
        {
            if (!audioSource.isPlaying || audioSource.clip != baseMusic)
            {
                audioSource.Stop();
                audioSource.clip = baseMusic;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.playerBottomLimit = -12;
            playerMovement.cameraRadius = 20;
            boss.SetActive(true);
            bossSpawned = true;
            healthBar.SetActive(true);
            //activate the stars
            stars1.isActive = true;
            stars2.isActive = true;
            //lock the camera movement
            cameraMovement.lockHorizontal = true;
            //zoom out the camera
            cameraMovement.ZoomOut();
        }
    }
}
