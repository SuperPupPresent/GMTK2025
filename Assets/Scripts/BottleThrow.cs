using UnityEngine;

public class BottleThrow : MonoBehaviour
{
    public float speed = 30f;
    public float maxDistance = 8f;
    public float returnHeight = 2f;
    private Vector3 startPosition;
    private Transform player;
    private bool returning = false;

    public void Launch(Vector3 direction, Transform playerTransform)
    {
        startPosition = transform.position;
        player = playerTransform;
        StartCoroutine(MoveBottle(direction));
    }

    private System.Collections.IEnumerator MoveBottle(Vector3 direction)
    {
        float traveled = 0f;
        while (traveled < maxDistance)
        {
            float step = speed * Time.deltaTime;
            transform.position += direction * step;
            traveled += step;
            yield return null;
        }

        yield return new WaitForSeconds(.5f); // Wait for a second before returning
        returning = true;

        Vector3 targetPosition = player.position + Vector3.up * returnHeight;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 returnDir = (targetPosition - transform.position).normalized;
            transform.position += returnDir * speed * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}