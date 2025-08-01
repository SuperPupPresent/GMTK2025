using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset playerInputActions;
    public GameObject lightHitbox; // Reference to the light attack hitbox
    public GameObject heavyHitbox; // Reference to the heavy attack hitbox
    public GameObject bottleHitbox; // Reference to the bottle hitbox for special attacks
    public GameObject recallHitbox; // Reference to the recall hitbox
    public GameObject playerContainer; // reference to container used for recall

    private GameManager gameManager; // Reference to the GameManager for health and mana management
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script for player movement
    PlayerHealth health;

    private AudioSource audioSource; // Reference to the AudioSource for playing sounds
    public AudioClip attackSound; // Sound for attack
    public AudioClip recallSound; // sound for special attacks
    public AudioClip longerReverse; // sound for recall
    public AudioClip bottleThrowSound; // sound for bottle throw


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
    bool readyingAttack = false;

    private float bottleThrowManaCost = 10f; // Mana cost for throwing a bottle
    private float recallManaCost = 50f; // Mana cost for recalling
    private float recallSpeed = 0.00001f;

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

        audioSource.clip = attackSound; // Set the attack sound
        float pitch = Random.Range(0.9f, 1.1f); // Random pitch for the attack sound
        audioSource.pitch = pitch; // Set the pitch for the attack sound
        audioSource.Play(); // Play the attack sound

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
        
        audioSource.clip = attackSound; // Set the attack sound
        float pitch = Random.Range(0.6f, 0.8f); // Random pitch for the attack sound
        audioSource.pitch = pitch; // Set the pitch for the attack sound
        audioSource.Play(); // Play the attack sound

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

        audioSource.clip = bottleThrowSound; // Set the bottle throw sound
        audioSource.pitch = 2.0f; // Set the pitch for the bottle throw sound
        audioSource.Play(); // Play the bottle throw sound

        Vector3 throwdirection = new Vector3(moveButton.ReadValue<Vector2>().x, 0, moveButton.ReadValue<Vector2>().y).normalized;
        GameObject bottle = Instantiate(bottleHitbox, bottleHitbox.transform.position, Quaternion.identity);
        BottleThrow bottleScript = bottle.GetComponent<BottleThrow>();
        bottleScript.Launch(throwdirection, this.transform);

        yield return new WaitForSeconds(0.5f); 
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

        audioSource.clip = longerReverse; // Set the recall sound
        audioSource.pitch = 1.2f; // Set the pitch for the recall sound
        audioSource.Play(); // Play the recall sound

        // move player to position from 2 seconds ago
        List<Vector3> recallPath = playerMovement.GetPositionsForRecall(2f); //get the list of positions
        for(int i = recallPath.Count -1; i >= 0; i--)
        {
            playerContainer.transform.position = recallPath[i]; // move player to each position in the list
            yield return new WaitForSeconds(recallSpeed);
        }
        
        gameManager.setHealth(gameManager.currentHealth + 20); // Heal the player by 20 health points after recall
        animator.SetBool("isRecall", false);
        recallHitbox.SetActive(true); // Activate the recall hitbox

        audioSource.clip = attackSound; // Set the attack sound for recall
        audioSource.pitch = 0.5f;
        audioSource.Play(); // Play the attack sound

        yield return new WaitForSeconds(0.5f); // Wait for the duration of the recall
        recallHitbox.SetActive(false); // Deactivate the recall hitbox
        isAttacking = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("PlayerAttack script started");
        lightHitbox.SetActive(false); // Ensure the hitbox is inactive at start
        heavyHitbox.SetActive(false); // Ensure the heavy hitbox is inactive at start
        bottleHitbox.SetActive(false); // Ensure the bottle hitbox is inactive at start
        recallHitbox.SetActive(false); // Ensure the recall hitbox is inactive at start

        gameManager = FindFirstObjectByType<GameManager>(); // Find the GameManager in the scene
        playerMovement = FindFirstObjectByType<PlayerMovement>(); // Find the PlayerMovement script in the scene
        health = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component for playing sounds

    }

    // Update is called once per frame
    void Update()
    {
        if (health.stunned)
        {
            return;
        }
        //if the light attack input is triggered, perform the light attack
        if ((lightAttack.WasPressedThisFrame() || readyingAttack) && !isAttacking)
        {
            //Debug.Log("Light Attack Triggered");
            readyingAttack = false;
            StartCoroutine(PerformLightAttack());
        }
        else if (lightAttack.WasPressedThisFrame())
        {
            readyingAttack = true;
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
            //Debug.Log("Grab Triggered");
            // Implement grab logic here
        }

        // Check for block input
        if (block.WasPressedThisFrame())
        {
            //Debug.Log("Block Triggered");
            // Implement block logic here
        }

    }
}
