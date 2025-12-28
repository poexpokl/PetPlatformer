using UnityEngine;

public class EnemyTeleportation : MonoBehaviour
{
    [SerializeField] Vector3 teleportationVector;

    public void Teleportation()
    {
        transform.position += teleportationVector;
    }

    public void ChangeTeleportationX() 
    {
        teleportationVector = new Vector3(-teleportationVector.x, teleportationVector.y, teleportationVector.z);
    }
}
