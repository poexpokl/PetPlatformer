using UnityEngine;

public class MainMenuExit : MenuButton
{    
    public override void ExecuteClick()
    {
        Application.Quit();
    }
}
