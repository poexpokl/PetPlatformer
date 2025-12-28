using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public void Teleport(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
    public void Teleport(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
