using System.Collections;
using UnityEngine;

public class AttackHitboxActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] hitboxes;
    [SerializeField] private float hitboxActiveTime;
    public void ActivateHitbox(int numberOfHitbox) //0 - вправо, 1 - влево, 2 - вверх, 3 - вниз
        //нейминг (тут же ещё деактивация)
    {
        hitboxes[numberOfHitbox].SetActive(true);
        StartCoroutine(DeactivateHitbox(numberOfHitbox));
    }

    IEnumerator DeactivateHitbox(int numberOfHitbox)
    {
        yield return new WaitForSeconds(hitboxActiveTime);
        hitboxes[numberOfHitbox].SetActive(false);
    }
}
