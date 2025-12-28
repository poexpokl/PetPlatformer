using System;
using UnityEngine;

public class EnemyDeathChecker : MonoBehaviour
{
    public static event Action EnemyDied;

    private void OnEnable()
    {
        GetComponent<EnemyHPManager>().Died += EnemyDied;
    }

    private void OnDisable()
    {
        GetComponent<EnemyHPManager>().Died -= EnemyDied;
    }
}
