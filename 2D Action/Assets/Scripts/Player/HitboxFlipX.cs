using UnityEngine;

public class HitboxFlipX : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerSpriteRenderer = G.player.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.flipX = playerSpriteRenderer.flipX;
    }
}
