using UnityEngine;

public class ArenaDisposable : Disposable
{
    protected override void DeactivateDisposable()
    {
        gameObject.SetActive(false);
    }
}
