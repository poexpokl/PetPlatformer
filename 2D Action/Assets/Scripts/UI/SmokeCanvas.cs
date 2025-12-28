using System.Collections;
using UnityEngine;

public class SmokeCanvas : MonoBehaviour
{
    [SerializeField] float smokeTime;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(ContinueGame());
    }
    private IEnumerator ContinueGame()
    {
        yield return new WaitForSeconds(smokeTime);

        Destroy(gameObject);
    }
}
