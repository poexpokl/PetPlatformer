using UnityEngine;

public class BossDisposable : Disposable
{
    protected override void DeactivateDisposable()
    {
        gameObject.SetActive(false);
    }
}
