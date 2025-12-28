using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage((Vector2)transform.position);
        }
    }
}
