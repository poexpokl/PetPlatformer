using UnityEngine;
using UnityEngine.EventSystems;

public interface IClickableButton : IPointerEnterHandler, IPointerExitHandler //Не нужен?
{
    public void ShowClickable();
    public void ShowUnclickable(); //?
}
