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
        if (PlayerController.currentState == PlayerState.Heal) //
        {
            float oldHeight = boxCollider.bounds.size.y;
            float newHeight = spriteRenderer.bounds.size.y; // Оно правильно работает?
            boxCollider.size = spriteRenderer.sprite.bounds.size; //может возникнуть рассинхрон при localScale x != y, тогда можно менять только y
            transform.position = new Vector2(transform.position.x, transform.position.y - (oldHeight - newHeight) / 2); //меняю transform?
        }
        else
        {
            if(boxCollider.size != generalColliderSize)//?
                boxCollider.size = generalColliderSize;
        }
    }
}
