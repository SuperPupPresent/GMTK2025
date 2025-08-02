using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    InputAction jumpButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpButton = InputSystem.actions.FindAction("jump");
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpButton.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Game");
        }
    }
}
