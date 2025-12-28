using UnityEngine;

public class MainMenuContinue : MenuButton
{
    public override void ExecuteClick()
    {
        SaveLoad.Instance.Load();
    }
}
