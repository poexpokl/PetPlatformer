using UnityEngine;

public class LastGroundPosiition : MonoBehaviour
{
    public Vector2 Position; //{ private get; set; }

    public void LastPositionTeleport()
    {
        transform.position = Position;
    }
}
