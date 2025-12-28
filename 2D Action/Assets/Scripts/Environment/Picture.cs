using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Picture : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject input;
    [SerializeField] GameObject picture;
    public void DoInteraction()
    {
        picture.SetActive(true);
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindAction("Pause").Enable();
    }

    public void HideInput()
    {
        if (input != null)
            input.SetActive(false);
    }

    public void ShowInput()
    {
        input.SetActive(true);
    }
}
