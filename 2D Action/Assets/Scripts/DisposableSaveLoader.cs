using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DisposableSaveLoader;

public class DisposableSaveLoader : MonoBehaviour //Название (тут не только SaveLoad)
{
    
    [System.Serializable]
    public class CompletedDisposables
    {
        [SerializeField] internal List<int> completedDisposables;
    }
    CompletedDisposables completedDisposables;
    public static DisposableSaveLoader Instance { get; private set; }
    private string pathToFile = "completedDisposable.json";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (File.Exists(Path.Combine(Application.persistentDataPath, pathToFile)))
        {
            completedDisposables = 
                JsonUtility.FromJson<CompletedDisposables>(File.ReadAllText(Path.Combine(Application.persistentDataPath, pathToFile)));

        }
        else
        {
            completedDisposables = new CompletedDisposables();
            completedDisposables.completedDisposables = new List<int>();
        }
    }

    public void SaveComplete(int id)
    {
        completedDisposables.completedDisposables.Add(id);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, pathToFile), JsonUtility.ToJson(completedDisposables));
    }

    public bool CheckID(int id)
    {
        return completedDisposables.completedDisposables.Contains(id);
    }

    public void DeleteSave()
    {
        if(File.Exists(Path.Combine(Application.persistentDataPath, pathToFile)))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, pathToFile));
        }
    }
}
