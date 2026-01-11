using UnityEngine;
using static PlayerController;

public class PlayerUpdateCollider : MonoBehaviour
{
    private PlayerController PlayerController;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Vector2 generalColliderSize;

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        generalColliderSize = boxCollider.size;
    }
    private void LateUpdate()
    {
        if (PlayerController.currentState == PlayerState.Heal)
        {
            float oldHeight = boxCollider.bounds.size.y;
            float newHeight = spriteRenderer.bounds.size.y;
            boxCollider.size = spriteRenderer.sprite.bounds.size;
            transform.position = new Vector2(transform.position.x, transform.position.y - (oldHeight - newHeight) / 2);
        }
        else
        {
            if(boxCollider.size != generalColliderSize)
                boxCollider.size = generalColliderSize;
        }
    }
}
