using System.Collections;
using UnityEngine;

public class BringerOfDeathProjectile : MonoBehaviour
{
    [SerializeField] float destroyTime = 2;
    [SerializeField] float addendRatio = 0.5f;
    private BoxCollider2D boxCollider;
    private void Start()
    {
        StartCoroutine(DestroyProjectile());
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float addend = Time.deltaTime * addendRatio; //помен€ть слагаемое, ввести startTime и endTime, помен€ть анимацию
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y + addend); 
        boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y + addend / 2);
    }
    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision != null)
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage((Vector2)transform.position);
        }
    }
}
