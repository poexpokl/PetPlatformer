using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    private InputAction pause; 
    void Awake()
    {
        menu.SetActive(true); //костыль
        menu.SetActive(false);
        pause = InputSystem.actions.FindAction("Pause");
        pause.Enable();
    }

    private void OnEnable()
    {
        pause.started += Open;
    }

    private void OnDisable()
    {
        pause.started -= Open;
    }

    private void Open(InputAction.CallbackContext context)
    {
        GetComponent<AudioSource>().Stop(); //??????????????
        PauseManager.Pause();
        menu.SetActive(true); //ћб в синглтон переделать или как-нибудь...
        menu.GetComponent<PauseMenu>().WasPlayerMapEnable = InputSystem.actions.FindAction("Move").enabled;
        InputSystem.actions.FindActionMap("Player").Disable(); // не соответствует названию
    }
}
