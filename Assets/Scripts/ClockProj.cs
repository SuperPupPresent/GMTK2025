using System.Collections;
using UnityEngine;

public class ClockProj : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
