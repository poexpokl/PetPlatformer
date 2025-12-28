using UnityEngine;

public class EnemyCheckPlayerSprite : MonoBehaviour
{
    SpriteRenderer playerSpriteRenderer;
    private void Start()
    {
        playerSpriteRenderer = G.player.GetComponent<SpriteRenderer>();
    }

    public bool isPlayerFlipX()
    {
        return playerSpriteRenderer.flipX;
    }
}
