using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset playerInputActions;
    public GameObject lightHitbox; // Reference to the light attack hitbox
    public GameObject heavyHitbox; // Reference to the heavy attack hitbox
    public GameObject bottleHitbox; // Reference to the bottle hitbox for special attacks

    private GameManager gameManager; // Reference to the GameManager for health and mana management

    private InputAction lightAttack;
    private InputAction heavyAttack;
    private InputAction specialAttack;
    private InputAction grab;
    private InputAction block;
    private InputAction moveButton;

    public Animator animator; // Reference to the Animator for player animations

    public bool isAttacking = false; // Flag to prevent multiple attacks at once
    private bool bottleThrown = false; // Flag to check if the bottle has been thrown
    private bool isLightRightNext = true; // Flag used for left right alternation

    private float bottleThrowManaCost = 10f; // Mana cost for throwing a bottle
    private float recallManaCost = 50f; // Mana cost for recalling

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
        moveButton = InputSystem.actions.FindAction("Move"); // arrow keys or joystick
    }


    private IEnumerator PerformLightAttack() 
    {
        animator.SetBool("isRunning", false);
        isAttacking = true;

        if(isLightRightNext)
        {
            animator.SetBool("isLightLeft", true);
        }
        else
        {
            animator.SetBool("isLightRight", true);
        }


        lightHitbox.SetActive(true);
        yield return new WaitForSeconds(0.2f); // Wait for the duration of the light attack
        lightHitbox.SetActive(false); // Deactivate the hitbox
        isAttacking = false;

        animator.SetBool("isLightLeft", false); 
        animator.SetBool("isLightRight", false); 
        isLightRightNext = !isLightRightNext; // alternation
    }

    private IEnumerator PerformHeavyAttack() 
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isHeavy", true);
        isAttacking = true;

        yield return new WaitForSeconds(0.1f); // Short delay before starting heavy attack
        
        heavyHitbox.SetActive(true); 
        yield return new WaitForSeconds(0.3f); // Wait for the duration of the heavy attack

        animator.SetBool("isHeavy", false);
        heavyHitbox.SetActive(false);
        
        isAttacking = false;

        
    }

    private IEnumerator PerformBottleThrow()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isBottleThrow", true);
        gameManager.setMana(-1 * bottleThrowManaCost); // Deduct mana cost for throwing a bottle

        isAttacking = true;
        bottleThrown = true; // Set the flag to true to prevent multiple throws
        bottleHitbox.SetActive(true); // Activate the bottle hitbox

        Vector3 throwdirection = new Vector3(moveButton.ReadValue<Vector2>().x, 0, moveButton.ReadValue<Vector2>().y).normalized;
        GameObject bottle = Instantiate(bottleHitbox, bottleHitbox.transform.position, Quaternion.identity);
        BottleThrow bottleScript = bottle.GetComponent<BottleThrow>();
        bottleScript.Launch(throwdirection, this.transform);

        yield return new WaitForSeconds(0.1f); 
        isAttacking = false;
        animator.SetBool("isBottleThrow", false);

        yield return new WaitForSeconds(1f); // Wait for a short duration before resetting
        bottleThrown = false; // Reset the flag after instantiation
        bottleHitbox.SetActive(false); // Deactivate the bottle hitbox after instantiation
    }

    private IEnumerator PerformRecall()
    {
        isAttacking = true;
        animator.SetBool("isRunning", false);
        animator.SetBool("isRecall", true);

        gameManager.setMana(-1 * recallManaCost); // Deduct mana cost for recall

        yield return new WaitForSeconds(1f); 
        animator.SetBool("isRecall", false);
        isAttacking = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("PlayerAttack script started");
        lightHitbox.SetActive(false); // Ensure the hitbox is inactive at start
        heavyHitbox.SetActive(false); // Ensure the heavy hitbox is inactive at start
        bottleHitbox.SetActive(false); // Ensure the bottle hitbox is inactive at start

        gameManager = FindFirstObjectByType<GameManager>(); // Find the GameManager in the scene

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

        // Check for special attack inputs
        if (specialAttack.WasPressedThisFrame() && (moveButton.ReadValue<Vector2>() != Vector2.zero) && !bottleThrown && gameManager.currentMana >= bottleThrowManaCost) //special + movement + bottle not thrown + enough mana
        {
            Debug.Log("Special Attack Bottle Throw Triggered");
            StartCoroutine(PerformBottleThrow());
        }
        if(specialAttack.WasPressedThisFrame() && (moveButton.ReadValue<Vector2>() == Vector2.zero) && gameManager.currentMana >= recallManaCost) //special + no movement + enough mana
        {
            Debug.Log("Special Attack Recall Triggered");
            StartCoroutine(PerformRecall());
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
