using UnityEngine;
using System.Collections;
using Unity.Mathematics;
public class FlyingDemonProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask destroyLayers;
    [SerializeField] float expodingTime;
    [SerializeField] float speed;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = (transform.rotation.eulerAngles.z) * Mathf.Deg2Rad;
        float xRatio = math.cos(angle);
        float yRatio = math.sin(angle);
        rb.linearVelocity = new Vector2(xRatio, yRatio) * speed;
        GetComponent<DestroyAtfter>().DestroyObject();
    }
    //сделать анимацию при попадании
    private IEnumerator DestroyProjectile()
    {
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForEndOfFrame();
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(expodingTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || ((1 << collision.gameObject.layer) & destroyLayers.value) != 0)
        {
            StartCoroutine(DestroyProjectile());
        }
    }
}
