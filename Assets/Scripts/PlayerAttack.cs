using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset playerInputActions;

    private InputAction lightAttack;
    private InputAction heavyAttack;
    private InputAction specialAttack;
    private InputAction grab;
    private InputAction block;

    private void OnEnable()
    {
        playerInputActions.FindActionMap("PlayerAttack").Enable();
    }

    private void OnDisable()
    {
        playerInputActions.FindActionMap("PlayerAttack").Disable();
    }

    private void Awake()
    {
        lightAttack = InputSystem.actions.FindAction("light");
        heavyAttack = InputSystem.actions.FindAction("heavy");
        specialAttack = InputSystem.actions.FindAction("special");
        grab = InputSystem.actions.FindAction("grab");
        block = InputSystem.actions.FindAction("block");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
