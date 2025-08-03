using UnityEngine;

public class StarMovement : MonoBehaviour
{
    float moveSpeed = 15;
    float outOfBound = 35;

    void Start()
    {
        //gameObject.SetActive(false);
    }

    void Update()
    {
        float newX = transform.position.x + -moveSpeed * Time.deltaTime;
        transform.position = new Vector2(newX, transform.position.y);
        Debug.Log(transform.localPosition.x);
        if(transform.localPosition.x < -outOfBound)
        {
            transform.localPosition = new Vector2(outOfBound, transform.localPosition.y);
        }
    }
}
