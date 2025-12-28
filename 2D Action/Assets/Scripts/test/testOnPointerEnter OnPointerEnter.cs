using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class testOnPointerEnterOnPointerEnter : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
    }
}
