using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    private float boxCastCenterY;
    private BoxCollider2D boxCollider;
    private Bounds bounds;
    [SerializeField] private float boxCastDistance;
    [SerializeField] private Vector2 boxCastSize = new Vector2(0.2f, 0.1f); // Ўирина и высота области проверки

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public bool Check()
    {
        bounds = boxCollider.bounds;
        boxCastCenterY = bounds.size.y / 2;
        Vector2 center = transform.position + Vector3.up * boxCastCenterY;
        RaycastHit2D hit = Physics2D.BoxCast(center, boxCastSize, 0f, Vector2.down, boxCastDistance, groundLayerMask);
        return hit.collider != null;
    }
}
