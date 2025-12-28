using Unity.Mathematics;
using UnityEngine;

public class EnemyChecks : MonoBehaviour //?
{
    private Transform playerTransform;
    private EnemyOrientation orientation;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float nearMagnitude;
    [SerializeField] private float nearX;
    [SerializeField] private float nearY;
    [SerializeField] private float activateMagnitude; //?
    [SerializeField] private float activateX;
    [SerializeField] private float activateY;

    private void Start()
    {
        playerTransform = G.playerTransform;
        orientation = GetComponent<EnemyOrientation>();
    }
    public bool CanSeePlayer() //переименовать
    {
        Vector2 playerVector = playerTransform.position - transform.position;
        Vector2 direction = playerVector.normalized;
        float distance = playerVector.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayers);

        return hit.collider == null;
    }
    public bool isPlayerNearCircle() //название??
    {
        return (playerTransform.position - transform.position).magnitude < nearMagnitude;
    }

    public bool isPlayerNearRectangle() //название??
    {
        return (math.abs(transform.position.x - playerTransform.position.x) < nearX &&
            math.abs(transform.position.y - playerTransform.position.y) < nearY);
    }

    public bool isPlayerNearRectangle(float newX, float newY) //название??
    {
        return (math.abs(transform.position.x - playerTransform.position.x) < newX &&
            math.abs(transform.position.y - playerTransform.position.y) < newY);
    }

    public bool isPlayerActivateCircle() //ТОЧНО НАЗВАНИЕ!
    {
        return (playerTransform.position - transform.position).magnitude < activateMagnitude;
    }

    public bool isPlayerActivateRectangle() //название??
    {
        return (math.abs(transform.position.x - playerTransform.position.x) < activateX &&
            math.abs(transform.position.y - playerTransform.position.y) < activateY);
    }

    public bool isPlayerRight()
    {
        return transform.position.x < playerTransform.position.x;
    }

    public bool isWrongOrientation()
    {
        return (isPlayerRight() && orientation.orientation == -1) || (!isPlayerRight() && orientation.orientation == 1);
    }
}
