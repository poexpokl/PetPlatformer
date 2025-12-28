using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : MonoBehaviour, IPlayerState
{
    [SerializeField] private float horizontalSpeed;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;
    private InputAction healAction;
    private InputAction interactAction;
    private float currentVelocity;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void EnterState()
    {
    }

    public void ExitState()
    {
    }

    public void StateFixedUpdate()
    {
        rb.linearVelocityX = currentVelocity;
    }

    public void StateUpdate()
    {
        currentVelocity = moveAction.ReadValue<float>() * horizontalSpeed;
        SetFlipX();
    }

    private void SetFlipX()
    {
        if (currentVelocity > 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipY = true;
    }
}
