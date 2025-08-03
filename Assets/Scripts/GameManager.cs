using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float maxPlayerHealth;
    [SerializeField] private float maxPlayerMana;

    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] private float manaRegenRate;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentMana;

    private AudioSource audioSource;
    public AudioClip gameOverSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen.SetActive(false);
        currentHealth = maxPlayerHealth;
        currentMana = maxPlayerMana;

        healthBar.fillAmount = currentHealth / maxPlayerHealth;
        manaBar.fillAmount = currentMana / maxPlayerMana;
        
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        RegenMana();
        if (currentHealth <= 0)
        {
            StartCoroutine(gameOver());
        }
        //setHealth(-1);
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
        Debug.Log("Health Change: " + healthChange);
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

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(1f);
        gameOverScreen.SetActive(true);
        //Play game over audio
        audioSource.pitch = 0.5f;
        audioSource.PlayOneShot(gameOverSound, 0.05f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");
    }
}
