using System.Collections;
using UnityEngine;

public class StrongWizardLaserStart : MonoBehaviour
{
    [SerializeField] float activeTime;

    private void Start()
    {
        StartCoroutine(UpdateCollider());
        StartCoroutine(DestroyLaser());
    }
    private IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(activeTime);

        Destroy(gameObject);
    }
    private IEnumerator UpdateCollider()
    {
        yield return new WaitForEndOfFrame();

        if (GetComponent<SpriteRenderer>().flipX)
            GetComponent<BoxCollider2D>().offset = -GetComponent<BoxCollider2D>().offset;
    }
}
