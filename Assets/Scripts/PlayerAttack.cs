using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset playerInputActions;
    public GameObject lightHitbox; // Reference to the light attack hitbox
    public GameObject heavyHitbox; // Reference to the heavy attack hitbox

    private InputAction lightAttack;
    private InputAction heavyAttack;
    private InputAction specialAttack;
    private InputAction grab;
    private InputAction block;

    private bool isAttacking = false; // Flag to prevent multiple attacks at once

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

        lightAttack = InputSystem.actions.FindAction("light"); // Z or West Button
        heavyAttack = InputSystem.actions.FindAction("heavy"); // X or North Button
        specialAttack = InputSystem.actions.FindAction("special"); // C or East Button
        grab = InputSystem.actions.FindAction("grab"); // A or Shoulder Button
        block = InputSystem.actions.FindAction("block"); // S or Trigger Button

    }


    private IEnumerator PerformLightAttack() 
    { 
        isAttacking = true; // Set attacking flag to true
        lightHitbox.SetActive(true); // Activate the hitbox
        yield return new WaitForSeconds(0.2f); // Wait for the duration of the light attack
        lightHitbox.SetActive(false); // Deactivate the hitbox
        isAttacking = false; // Reset attacking flag
    }

    private IEnumerator PerformHeavyAttack() 
    { 
        isAttacking = true; // Set attacking flag to true
        heavyHitbox.SetActive(true); // Activate the heavy hitbox
        yield return new WaitForSeconds(0.5f); // Wait for the duration of the heavy attack
        heavyHitbox.SetActive(false); // Deactivate the heavy hitbox
        isAttacking = false; // Reset attacking flag
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("PlayerAttack script started");
        lightHitbox.SetActive(false); // Ensure the hitbox is inactive at start
        heavyHitbox.SetActive(false); // Ensure the heavy hitbox is inactive at start

    }

    // Update is called once per frame
    void Update()
    {
        //if the light attack input is triggered, perform the light attack
        if (lightAttack.WasPressedThisFrame() && !isAttacking)
        {
            Debug.Log("Light Attack Triggered");
            StartCoroutine(PerformLightAttack());
        }
        // Check for heavy attack input
        if (heavyAttack.WasPressedThisFrame() && !isAttacking)
        {
            Debug.Log("Heavy Attack Triggered");
            StartCoroutine(PerformHeavyAttack());
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
