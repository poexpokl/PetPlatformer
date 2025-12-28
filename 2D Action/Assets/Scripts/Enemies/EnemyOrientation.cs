using UnityEngine;

public class EnemyOrientation : MonoBehaviour
{
    public int orientation {  get; private set; }
    private SpriteRenderer sprite;
    [SerializeField] private int startOrientation;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        orientation = startOrientation;
    }

    public void ChangeOrientation()
    {
        orientation *= -1;
        ChangeFlipX();
    }

    public void ChangeOrientation(int newOrientation)
    {
        orientation = newOrientation;
        if (orientation == startOrientation)
            ChangeFlipX(false);
        else
            ChangeFlipX(true);
    }
    
   private void ChangeFlipX()
    {
        sprite.flipX = !sprite.flipX;
    }

    private void ChangeFlipX(bool flipX)
    {
        sprite.flipX = flipX;
    }
}
