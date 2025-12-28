using System.Collections;
using UnityEngine;

public class SpikesPlatform : MonoBehaviour
{
    [SerializeField] float destroyTime;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject );
    }
}
