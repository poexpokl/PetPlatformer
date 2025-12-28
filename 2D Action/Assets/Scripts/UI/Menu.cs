using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected List<GameObject> buttonObjects;
    protected int currentButton;
    protected InputAction navigateAction; //1 или 2d axis
    protected InputAction pressAction;
    protected virtual void Awake()
    {
        navigateAction = InputSystem.actions.FindAction("Leaf");
        pressAction = InputSystem.actions.FindAction("Press");
    }
    protected virtual void OnEnable()
    {
        InputSystem.actions.FindActionMap("UI").Enable();
        navigateAction.started += PressNavigate; //поменять
        pressAction.started += PressClick;

        currentButton = 0;
        buttonObjects[currentButton].GetComponent<MenuButton>().ShowClickable();
    }

    protected virtual void OnDisable()
    {
        buttonObjects[currentButton].GetComponent<MenuButton>().ShowUnclickable();

        navigateAction.started -= PressNavigate;
        pressAction.started -= PressClick;
        InputSystem.actions.FindActionMap("UI").Disable();
    }
    protected void PressNavigate(InputAction.CallbackContext context)
    {
        int leafInput = (int)context.ReadValue<float>();
        int previousButton = currentButton; //number?
        currentButton += leafInput;
        if(currentButton < 0)
            currentButton = buttonObjects.Count - 1;
        if(currentButton > buttonObjects.Count - 1)
            currentButton = 0;
        buttonObjects[previousButton].GetComponent<MenuButton>().ShowUnclickable(); //?
        buttonObjects[currentButton].GetComponent<MenuButton>().ShowClickable();  
    }
    protected void PressClick(InputAction.CallbackContext context)
    {
        buttonObjects[currentButton].GetComponent<MenuButton>().ExecuteClick();
    }

    public void ChangeCurrentButtonFromMouse(GameObject button) //?
    {
        int previousButton = currentButton;
        currentButton = buttonObjects.IndexOf(button);
        buttonObjects[previousButton].GetComponent<MenuButton>().ShowUnclickable();
        buttonObjects[currentButton].GetComponent<MenuButton>().ShowClickable();
    }


}
