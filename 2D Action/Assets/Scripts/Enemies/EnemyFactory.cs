using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<Vector3> enemiesPosition;
    [SerializeField] private List<int> enemiesCounts;
    public ReadOnlyCollection<int> EnemiesCounts => enemiesCounts.AsReadOnly();
    public void CreateEnemies()
    {
        for (int i = 0; i < enemiesCounts[0]; i++) 
        {
            GameObject currentPostal = Instantiate(portal, enemiesPosition[0], Quaternion.identity);
            currentPostal.GetComponent<EnemyPortal>().enemy = enemies[0];
            enemies.RemoveAt(0);
            enemiesPosition.RemoveAt(0);
        }
        enemiesCounts.RemoveAt(0);
    }
}
