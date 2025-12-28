using UnityEngine;

public class EdgeTrigger : MonoBehaviour
{
    IEdgeble edgeble;
    [SerializeField] MonoBehaviour edgebleScript;
    [SerializeField] int orientation;

    private void OnTriggerExit2D(Collider2D collision)
    {
        edgeble = edgebleScript as IEdgeble;
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 11) //ground || wall
        {
            edgeble.OnEdge(orientation);
        }
    }
}
