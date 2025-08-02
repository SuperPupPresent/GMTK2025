using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;

    public bool lockHorizontal;
    private float xPos;

    public float smoothSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockHorizontal = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (lockHorizontal)
        {
            transform.position = new Vector3(xPos, 0, -10);
        }

        else
        {
            xPos = Mathf.Lerp(xPos, playerTransform.position.x, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(xPos, 0, -10);
            
        }
        
    }

}
