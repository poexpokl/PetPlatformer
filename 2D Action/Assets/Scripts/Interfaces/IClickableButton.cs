using UnityEngine.EventSystems;

public interface IClickableButton : IPointerEnterHandler, IPointerExitHandler
{
    public void ShowClickable();
    public void ShowUnclickable();
}
