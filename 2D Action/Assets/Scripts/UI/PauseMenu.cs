using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : Menu
{
    private InputAction exitAction; //button

    public bool WasPlayerMapEnable; //{ private get; set; } //?

    protected override void Awake()
    {
        base.Awake();
        exitAction = InputSystem.actions.FindAction("Exit");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        exitAction.started += PressExit;
    }

    protected override void OnDisable()
    {
        exitAction.started -= PressExit;
        base.OnDisable();
    }
    private void PressExit(InputAction.CallbackContext context)
    {
        buttonObjects[0].GetComponent<MenuButton>().ExecuteClick(); //?
    }
    public void CloseMenu()
    {
        if (WasPlayerMapEnable)
            InputSystem.actions.FindActionMap("Player").Enable();
        else
            InputSystem.actions.FindAction("Pause").Enable();
        PauseManager.Unpause();
        gameObject.SetActive(false);
    }
}
