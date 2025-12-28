using System.Collections;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    private float portalTime = 10f / 9;
    public GameObject enemy { private get; set; }

    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(portalTime);

        GameObject currentEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        currentEnemy.AddComponent<EnemyDeathChecker>();
        Destroy(gameObject);
    }
}
