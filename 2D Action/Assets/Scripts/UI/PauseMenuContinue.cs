using UnityEngine;

public class PauseMenuContinue : MenuButton
{
    public override void ExecuteClick()
    {
        GetComponentInParent<PauseMenu>().CloseMenu();
    }
}
