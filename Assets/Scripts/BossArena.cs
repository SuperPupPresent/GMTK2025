using UnityEngine;

public class BossArena : MonoBehaviour
{

    public CameraMovement cameraMovement;
    public GameObject boss;

    public bool bossSpawned = false;

    private AudioSource audioSource;
    public AudioClip bossMusic;
    public AudioClip baseMusic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            boss.SetActive(true);
            bossSpawned = true;

            cameraMovement.lockHorizontal = true;
            //zoom out the camera
            cameraMovement.ZoomOut();
        }
    }
}
