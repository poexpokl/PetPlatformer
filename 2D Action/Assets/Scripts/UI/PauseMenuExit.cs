using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuExit : MenuButton
{
    public override void ExecuteClick()
    {
        if (GetComponentInParent<PauseMenu>().WasPlayerMapEnable)
            InputSystem.actions.FindActionMap("Player").Enable();
        else
            InputSystem.actions.FindAction("Pause").Enable();
        PauseManager.Unpause();
        SceneManager.LoadScene(0);
    }
}
