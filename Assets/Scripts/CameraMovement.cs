using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;

    public bool zoom = false;
    public bool lockHorizontal;
    private float xPos;

    public float smoothSpeed = 5f;

    public float zoomSpeed = 2f;
    public float zoomSize = 12f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockHorizontal = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player can't walk out of bounds
        if(playerTransform.position.x < -10)
        {
            StartCoroutine(ContainPlayer());
        }

        if (lockHorizontal)
        {
            transform.position = new Vector3(xPos, 0, -10);
        }

        else
        {
            xPos = Mathf.Lerp(xPos, playerTransform.position.x, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(xPos, 0, -10);
            
        }

        if (zoom)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomSize, zoomSpeed * Time.deltaTime);
        }
        
    }

    public void ZoomOut()
    {
        //zoom out the camera
        zoom = true;
    }

    IEnumerator ContainPlayer()
    {
        lockHorizontal = true;
        yield return new WaitUntil(() => playerTransform.position.x < -10);
        lockHorizontal = false;
    }
}
