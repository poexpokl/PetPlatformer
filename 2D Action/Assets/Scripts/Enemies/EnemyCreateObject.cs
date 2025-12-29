using System.Collections;
using UnityEngine;

public class EnemyCreateObject : MonoBehaviour 
{
    [SerializeField] private GameObject createdObject;
    [SerializeField] private Vector3 objectOffset;
    [SerializeField] private float createTime;
    [SerializeField] private float attackTime;
    [SerializeField] private float cooldownTime;
    public bool isAttacking {  get; private set; } = false;
    public bool isCooldown { get; private set; } = false;
    private EnemyOrientation orientation;

    private void Start()
    {
        orientation = GetComponent<EnemyOrientation>();
    }
    public void CreateObject()
    {
        StartCoroutine(CreatingObject());
    }

    public void CreateFlippedObject()
    {
        StartCoroutine(CreatingFlippedObject());
    }

    IEnumerator CreatingObject()
    {
        isAttacking = true;
        isCooldown = true;
        yield return new WaitForSeconds(createTime);
        Instantiate(createdObject, transform.position + 
            new Vector3(objectOffset.x * orientation.orientation, objectOffset.y, objectOffset.z) , Quaternion.identity);
        yield return new WaitForSeconds(attackTime - createTime);
        isAttacking = false; 
        yield return new WaitForSeconds(cooldownTime - attackTime);
        isCooldown = false;
    }
    IEnumerator CreatingFlippedObject()
    {
        //isAttacking = true;
        //isCooldown = true;
        yield return new WaitForSeconds(createTime);
        Vector3 newObjectOffset = new Vector3(-objectOffset.x * orientation.orientation, objectOffset.y, objectOffset.z);
        GameObject newObject = Instantiate(createdObject, transform.position + newObjectOffset, Quaternion.identity);
        newObject.GetComponent<SpriteRenderer>().flipX = true;
        //yield return new WaitForSeconds(attackTime - createTime);
        //isAttacking = false; //есть ли случаи, когда это не подходит???
        //yield return new WaitForSeconds(cooldownTime - attackTime);
        //isCooldown = false;
    }
    public void CreateObject(int orientation)
    {
        StartCoroutine(CreatingObject(orientation));
    }

    public void CreateFlippedObject(int orientation)
    {
        StartCoroutine(CreatingFlippedObject(orientation));
    }

    IEnumerator CreatingObject(int orientation)
    {
        isAttacking = true;
        isCooldown = true;
        yield return new WaitForSeconds(createTime);
        Instantiate(createdObject, transform.position +
            new Vector3(objectOffset.x * orientation, objectOffset.y, objectOffset.z), Quaternion.identity);
        yield return new WaitForSeconds(attackTime - createTime);
        isAttacking = false;
        yield return new WaitForSeconds(cooldownTime - attackTime);
        isCooldown = false;
    }
    IEnumerator CreatingFlippedObject(int orientation)
    {
        //isAttacking = true;
        //isCooldown = true;
        yield return new WaitForSeconds(createTime);
        Vector3 newObjectOffset = new Vector3(-objectOffset.x * orientation, objectOffset.y, objectOffset.z);
        GameObject newObject = Instantiate(createdObject, transform.position + newObjectOffset, Quaternion.identity);
        newObject.GetComponent<SpriteRenderer>().flipX = true;
        //yield return new WaitForSeconds(attackTime - createTime);
        //isAttacking = false; //есть ли случаи, когда это не подходит???
        //yield return new WaitForSeconds(cooldownTime - attackTime);
        //isCooldown = false;
    }

    public void CreateObject(Transform targetTransform, int multiplier) //из точки //таргет“рансформ???????
    {
        StartCoroutine(CreatingObject(targetTransform, multiplier));
    }

    IEnumerator CreatingObject(Transform targetTransform, int multiplier)
    {
        isAttacking = true;
        isCooldown = true;
        yield return new WaitForSeconds(createTime);
        Instantiate(createdObject, targetTransform.position + 
            new Vector3(objectOffset.x * multiplier, objectOffset.y, objectOffset.z), Quaternion.identity);
        yield return new WaitForSeconds(attackTime - createTime);
        isAttacking = false; 
        yield return new WaitForSeconds(cooldownTime - attackTime);
        isCooldown = false;
    }

    public void CreateObject(Transform targerTransform) //из шутера под углом
    {
        StartCoroutine(CreatingObject(targerTransform));
    }

    IEnumerator CreatingObject(Transform targerTransform) 
    {
        isAttacking = true;
        isCooldown = true;
        yield return new WaitForSeconds(createTime);
        Vector2 projectileDirection = (targerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
        Instantiate(createdObject, transform.position + 
            new Vector3(objectOffset.x * orientation.orientation, objectOffset.y, objectOffset.z), Quaternion.Euler(0f, 0f, angle));
        yield return new WaitForSeconds(attackTime - createTime);
        isAttacking = false;
        yield return new WaitForSeconds(cooldownTime - attackTime);
        isCooldown = false;
    }
    /*public float CalculateAngle(Vector3 targetPosition)
    {
        Vector2 projectileDirection = (targetPosition - transform.position).normalized;
        return Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
    }*/
    public void StopCoroutines()
    {
        StopAllCoroutines();
        isAttacking = false;
        isCooldown = false;
    }
}
