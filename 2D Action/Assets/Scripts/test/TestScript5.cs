using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using static DisposableSaveLoader;

public class TestScript5 : MonoBehaviour
{
    private string pathToFile = "completedDisposable.json";
    public void Remove()
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, pathToFile), JsonUtility.ToJson(new CompletedDisposables()));
    }
}
