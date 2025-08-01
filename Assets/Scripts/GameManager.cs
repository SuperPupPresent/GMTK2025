using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float maxPlayerHealth;
    [SerializeField] private float maxPlayerMana;

    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;

    [SerializeField] private float manaRegenRate;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentMana;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxPlayerHealth - 40;
        currentMana = maxPlayerMana - 40;

        healthBar.fillAmount = currentHealth / maxPlayerHealth;
        manaBar.fillAmount = currentMana / maxPlayerMana;
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
            setMana(Mathf.Clamp(manaRegenRate * Time.deltaTime, 0, 100));
        }
    }

    public void setHealth(float healthChange)
    {
        currentHealth += healthChange;
        if(currentHealth > maxPlayerHealth)
        {
            currentHealth = maxPlayerHealth;
        }
        healthBar.fillAmount = currentHealth / maxPlayerHealth;
    }

    public void setMana(float manaChange)
    {
        currentMana += manaChange;
        if (currentMana > maxPlayerMana)
        {
            currentMana = maxPlayerMana;
        }
        manaBar.fillAmount = currentMana / maxPlayerMana;
    }
}
