using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    InputAction pauseButton;

    private void Awake()
    {
        pauseButton = InputSystem.actions.FindAction("Pause");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Check for escape key press to open and close pause menu
        if (pauseButton.WasPressedThisFrame())
        {
            togglePause();
        }
    }

    public void togglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 1 - Time.timeScale;
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        //Open Menu Scene
    }

    public void Quit()
    {
        //Debug.Log("Quit Game");
        Application.Quit();
    }
}
