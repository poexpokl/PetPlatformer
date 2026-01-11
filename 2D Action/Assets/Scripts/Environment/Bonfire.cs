using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bonfire : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject input;
    [SerializeField] private float loadTime;
    [SerializeField] private float smokeTime;
    [SerializeField] private GameObject smokeCanvas;
    public void DoInteraction()
    {
        SaveLoad.Instance.Save();
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindAction("Pause").Enable();
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(smokeTime);
        Instantiate(smokeCanvas);
        yield return new WaitForSeconds(loadTime - smokeTime);
        Vector3 playerPosition = G.playerTransform.position;
        int mana = G.player.GetComponent<ResourcesManager>().mana;
        bool flipX = G.player.GetComponent<SpriteRenderer>().flipX;
        InputSystem.actions.FindActionMap("Player").Enable();
        SaveLoad.Instance.Reload(playerPosition, mana, flipX);
    }

    public void ShowInput()
    {
        input.SetActive(true);
    }
    public void HideInput()
    {
        if(input != null)
            input.SetActive(false);
    }
}
