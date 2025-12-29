using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad Instance { get; private set; }
    private SaveData saveData;
    private string pathToFile = "save.json";

    [System.Serializable]
    private class SaveData 
    {
        [SerializeField] internal int sceneIndex;
        [SerializeField] internal Vector3 playerPosition;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        saveData.playerPosition = G.playerTransform.position;
        File.WriteAllText(Path.Combine(Application.persistentDataPath, pathToFile), JsonUtility.ToJson(saveData));
        //Debug.Log($"Save\nsaveData.sceneIndex: {saveData.sceneIndex}, saveData.playerPosition: {saveData.playerPosition}");
    }

    public void Load()
    {
        if (CheckSaveFileExists())
        {
            saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(Path.Combine(Application.persistentDataPath, pathToFile)));
            //Debug.Log($"saveData.sceneIndex: {saveData.sceneIndex}, saveData.playerPosition: {saveData.playerPosition}");
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(saveData.sceneIndex);
        }
        else
            SceneManager.LoadScene(1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        G.playerTransform.position = saveData.playerPosition;
    }

    Vector3 playerPosition;
    int hp;
    int mana;
    bool flipX;
    public void Load(Vector3 playerPosition, int hp, int mana, int SceneNumber, bool flipX)
    {
        this.playerPosition = playerPosition;
        this.hp = hp;
        this.mana = mana;
        this.flipX = flipX;
        SceneManager.sceneLoaded += OnSceneOverloadingLoaded;
        SceneManager.LoadScene(SceneNumber);
    }
    private void OnSceneOverloadingLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneOverloadingLoaded;
        G.playerTransform.position = playerPosition; //решить какой способ выбрать. Этот (с контролем здесь) или другой
        G.player.GetComponent<ResourcesManager>().ChangeHp(hp - 5); //5?
        G.player.GetComponent<ResourcesManager>().ChangeMana(mana);
        G.player.GetComponent<SpriteRenderer>().flipX = flipX;
    }

    public void Reload(Vector3 playerPosition, int mana, bool flipX)
    {
        this.playerPosition = playerPosition;
        this.mana = mana;
        this.flipX = flipX;
        SceneManager.sceneLoaded += OnSceneReloaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneReloaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneReloaded;
        G.player.GetComponent<PlayerReloading>().Reload(playerPosition, mana, flipX);
    }

    public void DeleteSave()
    {
        if (CheckSaveFileExists())
        {
            File.Delete(Path.Combine(Application.persistentDataPath, pathToFile));
        }
    }

    public bool CheckSaveFileExists()
    {
        return File.Exists(Path.Combine(Application.persistentDataPath, pathToFile));
    }
}
