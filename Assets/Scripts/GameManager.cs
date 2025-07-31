using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float maxPlayerHealth;
    [SerializeField] float maxPlayerMana;

    public float currentHealth;
    public float currentMana;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxPlayerHealth;
        currentMana = maxPlayerMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
