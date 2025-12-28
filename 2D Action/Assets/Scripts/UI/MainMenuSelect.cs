using UnityEngine;

public class MainMenuSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] menu;
    void Start()
    {
        if(SaveLoad.Instance.CheckSaveFileExists())
            menu[0].SetActive(true);
        else
            menu[1].SetActive(true);
    }
}
