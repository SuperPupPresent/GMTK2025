using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset playerInputActions;
    public GameObject lightHitbox; // Reference to the light attack hitbox
    public float lightDuration = 0.5f; // Duration for the light attack hitbox

    private InputAction lightAttack;
    private InputAction heavyAttack;
    private InputAction specialAttack;
    private InputAction grab;
    private InputAction block;

    private void OnEnable()
    {
        playerInputActions.FindActionMap("PlayerAttack").Enable();
        Debug.Log("PlayerAttack action map enabled");
    }

    private void OnDisable()
    {
        playerInputActions.FindActionMap("PlayerAttack").Disable();
    }

    private void Awake()
    {
        Debug.Log("PlayerAttack script awake");
        playerInputActions.FindActionMap("PlayerAttack").Enable();

        lightAttack = InputSystem.actions.FindAction("light"); // Z or West Button
        heavyAttack = InputSystem.actions.FindAction("heavy"); // X or North Button
        specialAttack = InputSystem.actions.FindAction("special"); // C or East Button
        grab = InputSystem.actions.FindAction("grab"); // A or Shoulder Button
        block = InputSystem.actions.FindAction("block"); // S or Trigger Button

    }


    private IEnumerator PerformLightAttack() 
    { 
        lightHitbox.SetActive(true); // Activate the hitbox
        yield return new WaitForSeconds(lightDuration); // Wait for the duration of the light attack
        lightHitbox.SetActive(false); // Deactivate the hitbox
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("PlayerAttack script started");
        lightHitbox.SetActive(false); // Ensure the hitbox is inactive at start
       
    }

    // Update is called once per frame
    void Update()
    {
        //if the light attack input is triggered, perform the light attack
        if (lightAttack.WasPressedThisFrame())
        {
            Debug.Log("Light Attack Triggered");
            StartCoroutine(PerformLightAttack());
        }
        // Check for heavy attack input
        if (heavyAttack.WasPressedThisFrame())
        {
            Debug.Log("Heavy Attack Triggered");
            // Implement heavy attack logic here
        }
        // Check for special attack input
        if (specialAttack.WasPressedThisFrame())
        {
            Debug.Log("Special Attack Triggered");
            // Implement special attack logic here
        }
        // Check for grab input
        if (grab.WasPressedThisFrame())
        {
            Debug.Log("Grab Triggered");
            // Implement grab logic here
        }
        // Check for block input
        if (block.WasPressedThisFrame())
        {
            Debug.Log("Block Triggered");
            // Implement block logic here
        }

    }
}
