using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float maxPlayerHealth;
    [SerializeField] private float maxPlayerMana;

    [SerializeField] private float manaRegenRate;

    public float currentHealth;
    public float currentMana;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxPlayerHealth;
        currentMana = maxPlayerMana - 40;
    }

    // Update is called once per frame
    void Update()
    {
        RegenMana();

        //Debug.Log("Health: " + currentHealth);
        //Debug.Log("Mana: " + currentMana);

    }

    void RegenMana()
    {
        if (currentMana < maxPlayerMana)
        {
            currentMana = Mathf.Clamp(currentMana + manaRegenRate * Time.deltaTime, 0, 100);
        }
    }
}
