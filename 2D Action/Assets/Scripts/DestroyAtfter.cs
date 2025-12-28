using System.Collections;
using UnityEngine;

public class DestroyAtfter : MonoBehaviour
{
    [SerializeField] float destroyTime;
    public void DestroyObject()
    {
        StartCoroutine(DestroyingObject());
    }
    private IEnumerator DestroyingObject()
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
