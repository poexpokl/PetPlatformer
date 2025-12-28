using UnityEngine.SceneManagement;

public class MainMenuNewGame : MenuButton
{
    public override void ExecuteClick()
    {
        SaveLoad.Instance.DeleteSave();
        DisposableSaveLoader.Instance.DeleteSave();
        SceneManager.LoadScene(1);
    }
}
