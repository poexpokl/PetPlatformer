using Unity.Cinemachine;
using UnityEngine;

public class BossArena : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject[] bounds;
    [SerializeField] private Collider2D cameraBounds;
    [SerializeField] private GameObject cinemachineCamera;
    private CinemachineConfiner2D confiner;
    private Collider2D previousCollider;

    private void OnEnable()
    {
        boss.SetActive(true); //????????
        boss.GetComponent<NecromancerHPManager>().Died += BossDied; //добавить интерфейс для события
        boss.SetActive(false);
        //а ещё босс выключен. Как я с него компонент возьму?
    }

    private void OnDisable()
    {
        boss.GetComponent<NecromancerHPManager>().Died -= BossDied;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            boss.SetActive(true);
            foreach (GameObject bound in bounds) 
            { 
                bound.SetActive(true);
            }
            confiner = cinemachineCamera.GetComponent<CinemachineConfiner2D>();
            previousCollider = confiner.BoundingShape2D;
            confiner.BoundingShape2D = cameraBounds;
        }
    }

    private void BossDied()
    {
        foreach (GameObject bound in bounds)
        {
            bound.SetActive(false);
        }
        confiner.BoundingShape2D = previousCollider;
        GetComponent<BossDisposable>().SetCompleted();
        Destroy(gameObject);
    }
}
