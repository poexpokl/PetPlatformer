using UnityEngine;

public class SetLastGround : MonoBehaviour
{
    [SerializeField] private Vector2 spawnPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<LastGroundPosiition>().Position = spawnPosition;
        }
    }
}
