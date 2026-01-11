using System.Collections;
using UnityEngine;

public class StrongWizardLaser : MonoBehaviour
{
    [SerializeField] float activeTime;
    private void Start()
    {
        StartCoroutine(DestroyLaser());
    }
    private IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(activeTime);

        Destroy(gameObject);
    }
}
