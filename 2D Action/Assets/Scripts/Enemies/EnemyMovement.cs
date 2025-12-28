using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed { get; private set;}
    private Rigidbody2D rb;
    private EnemyOrientation enemyOrientation;
    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    private bool isUpdateBlocked;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyOrientation = GetComponent<EnemyOrientation>();
    }

    private void FixedUpdate()
    {
        if (!isUpdateBlocked)
            rb.linearVelocityX = speed * enemyOrientation.orientation;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetRunSpeed()
    {
        speed = runSpeed;
    }

    public void SetWalkSpeed()
    {
        speed = walkSpeed;
    }

    public void SetZeroSpeed()
    {
        speed = 0f;
    }

    public void SetZeroLinearVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void EnemyAddForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void SetIsUpdateBlocked(bool newIsUpdateBlocked)
    {
        isUpdateBlocked = newIsUpdateBlocked;
    }
}
