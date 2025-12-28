using System;
using UnityEngine;

public class ArenaWaves : MonoBehaviour
{
    private int enemyCounts;
    public event Action EndWaves;
    [SerializeField] private EnemyFactory enemyFactory;
    private void OnEnable()
    {
        EnemyDeathChecker.EnemyDied += EnemyDied;
    }

    private void OnDisable()
    {
        EnemyDeathChecker.EnemyDied -= EnemyDied;
    }
    public void StartWaves()
    {
        enemyCounts = enemyFactory.EnemiesCounts[0];
        enemyFactory.CreateEnemies();
    }
    private void NextWave() //?
    {
        if(enemyFactory.EnemiesCounts.Count > 0)
        {
            enemyCounts = enemyFactory.EnemiesCounts[0];
            enemyFactory.CreateEnemies();
        }
        else
        {
            EndWaves?.Invoke();
        }
    }

    private void EnemyDied()
    {
        enemyCounts--;
        if(enemyCounts == 0)
            NextWave();
    }
}
