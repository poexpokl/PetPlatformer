using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage((Vector2)transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage((Vector2)transform.position);
        }
    }
}
