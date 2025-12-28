using System;
using UnityEngine;

public class DeadChecker : MonoBehaviour
{
    public event Action Died;
    private void OnDestroy()
    {
        Died?.Invoke();
    }
}
