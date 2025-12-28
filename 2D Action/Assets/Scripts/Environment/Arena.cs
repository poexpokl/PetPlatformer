using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Arena : MonoBehaviour //Название?
{
    [SerializeField] private ArenaEnter arenaEnter;
    [SerializeField] private ArenaWaves arenaWaves;
    [SerializeField] private GameObject[] ObjectsToActivate;
    [SerializeField] private Collider2D cameraBounds;
    [SerializeField] private GameObject cinemachineCamera;
    [SerializeField] private float waveDelay = 0.5f;
    private CinemachineConfiner2D confiner;
    private Collider2D previousCollider;

    private void OnEnable()
    {
        arenaEnter.OnArena += StartArena;
        arenaWaves.EndWaves += EndArena;
    }

    private void OnDisable()
    {
        arenaEnter.OnArena -= StartArena;
        arenaWaves.EndWaves -= EndArena;
    }

    private void StartArena()
    {
        foreach (GameObject objectToActivate in ObjectsToActivate)
        {
            objectToActivate.SetActive(true);
        }
        confiner = cinemachineCamera.GetComponent<CinemachineConfiner2D>();
        previousCollider = confiner.BoundingShape2D;
        confiner.BoundingShape2D = cameraBounds;
        StartCoroutine(StartWaves());
    }

    private IEnumerator StartWaves()
    {
        yield return new WaitForSeconds(waveDelay);
        arenaWaves.StartWaves();
    }
    private void EndArena()
    {
        foreach (GameObject objectToActivate in ObjectsToActivate) //Activate?
        {
            objectToActivate.SetActive(false);
        }
        confiner.BoundingShape2D = previousCollider;
        GetComponent<ArenaDisposable>().SetCompleted();
    }
}
