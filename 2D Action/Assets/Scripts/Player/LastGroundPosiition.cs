using UnityEngine;

public class LastGroundPosiition : MonoBehaviour
{
    public Vector2 Position;

    public void LastPositionTeleport()
    {
        transform.position = Position;
    }
}
