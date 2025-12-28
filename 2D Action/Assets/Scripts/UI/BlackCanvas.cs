using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlackCanvas : MonoBehaviour
{
    [SerializeField] float destroyTime;
    void Start()
    {
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindAction("Pause").Enable();
        StartCoroutine(DestroyCanvas());
    }

    private IEnumerator DestroyCanvas()
    {
        yield return new WaitForSeconds(destroyTime / 2);
        G.player.GetComponent<LastGroundPosiition>().LastPositionTeleport();
        yield return new WaitForSeconds(destroyTime / 2);
        InputSystem.actions.FindActionMap("Player").Enable();
        Destroy(gameObject);
    }

}
