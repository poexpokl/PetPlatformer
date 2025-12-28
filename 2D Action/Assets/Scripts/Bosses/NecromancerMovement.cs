using System;
using UnityEngine;

public class NecromancerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2[] points;
    [SerializeField] private float moveTime;
    public event Action Moved;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void MoveToPoint(int pointNumber)
    {
        animator.SetBool("isMoving", true);
        rb.linearVelocity = (points[pointNumber] - (Vector2)transform.position) / moveTime;
        if(rb.linearVelocityX < 0)
            spriteRenderer.flipX = true;
        Invoke("EndMove", moveTime);
    }

    private void EndMove()
    {
        animator.SetBool("isMoving", false);
        spriteRenderer.flipX = false;
        rb.linearVelocity = Vector2.zero;
        Moved?.Invoke();
    }
}
