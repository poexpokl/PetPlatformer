using UnityEngine;

public class test4 : MonoBehaviour
{
    public void Save()
    {
        SaveLoad.Instance.Save();
    }

    public void Load()
    {
        SaveLoad.Instance.Load();
    }
}
