using UnityEngine;

public abstract class Disposable : MonoBehaviour
{
    [SerializeField] protected int id;

    protected void Start()
    {
        if (DisposableSaveLoader.Instance.CheckID(id))
            DeactivateDisposable();
    }
    public void SetCompleted()
    {
        DisposableSaveLoader.Instance.SaveComplete(id);
    }

    protected abstract void DeactivateDisposable();
}
