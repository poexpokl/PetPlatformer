using System.Collections;
using UnityEngine;

public class StrongWizardLaserEnd : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] float activeTime;
    private Vector2 direction;
    private float distance = 35f;
    private float currentDistance;
    private float startSize = 12.299f - 8.847f;
    //private float scaleScalingSpeed;
    //private float currentTime;
    private void Start()
    {
        StartCoroutine(SetLocalScale()); //изменить название
        StartCoroutine(DestroyLaser());
    }
    private IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(activeTime);

        Destroy(gameObject);
    }
    private IEnumerator SetLocalScale() //название?
    {
        yield return new WaitForEndOfFrame();

        if (GetComponent<SpriteRenderer>().flipX)
        {
            direction = Vector2.left;
            GetComponent<BoxCollider2D>().offset = -GetComponent<BoxCollider2D>().offset;
        }
        else
            direction = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayers);
        if(hit.collider != null)
            currentDistance = hit.distance;
        else
            currentDistance = distance;
        //scaleScalingSpeed = currentDistance / startSize / activeTime;
        transform.localScale = new Vector3(currentDistance / startSize, transform.localScale.y, transform.localScale.z);
    }

    /*private void Update()
    {
        transform.localScale = new Vector3(scaleScalingSpeed * currentTime, transform.localScale.y, transform.localScale.z);
        currentTime += Time.deltaTime;
    }*/
}
