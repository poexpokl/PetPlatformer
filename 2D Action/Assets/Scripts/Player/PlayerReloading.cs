using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerReloading : MonoBehaviour
{

    public void Reload(Vector3 playerPosition, int mana, bool flipX)
    {
        StartCoroutine(Reloading(playerPosition, mana, flipX));
    }
    private IEnumerator Reloading(Vector3 playerPosition, int mana, bool flipX)
    {
        yield return new WaitForNextFrameUnit();

        transform.position = playerPosition;
        GetComponent<ResourcesManager>().ChangeMana(mana);
        GetComponent<PlayerController>().SetInteractState();
        GetComponent<SpriteRenderer>().flipX = flipX;
        GetComponent<Animator>().Play("Heal");
    }
}
