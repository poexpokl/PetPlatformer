using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerReloading : MonoBehaviour
{

    public void Reload(Vector3 playerPosition, int mana, bool flipX) //????????????????????????????????????????
    {
        /*StackTrace stackTrace = new StackTrace();

        // Кто вызвал этот метод (1 уровень назад)
        StackFrame callerFrame = stackTrace.GetFrame(1);
        MethodBase callerMethod = callerFrame.GetMethod();

        UnityEngine.Debug.Log($"Метод вызван из: {callerMethod.DeclaringType.Name}.{callerMethod.Name}"); */

        StartCoroutine(Reloading(playerPosition, mana, flipX));
        //Debug.Log("Reload");
        //анимация
    }
    private IEnumerator Reloading(Vector3 playerPosition, int mana, bool flipX)
    {
        yield return new WaitForNextFrameUnit();

        transform.position = playerPosition;
        GetComponent<ResourcesManager>().ChangeMana(mana);
        GetComponent<PlayerController>().SetInteractState();
        GetComponent<SpriteRenderer>().flipX = flipX;
        GetComponent<Animator>().Play("Heal"); //поменять
    }
}
