using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler//, IPointerExitHandler
//IClickableButton, IExecutableButon
{
    private TMP_Text textComponent;
    private Color startColor;
    private void Awake()
    {
        if(textComponent == null) //?
        {
            textComponent = GetComponent<TMP_Text>();
            startColor = textComponent.color;
        }

    }
    public abstract void ExecuteClick();
    public void ShowClickable()
    {
        if(textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();//?
            startColor = textComponent.color;
        }

        textComponent.color = Color.white;
    }

    public void ShowUnclickable()
    {
        textComponent.color = startColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<Menu>().ChangeCurrentButtonFromMouse(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ExecuteClick();
    }
}
