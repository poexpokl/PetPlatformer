using Unity.Cinemachine;
using UnityEngine;

public class ParallaxMain : MonoBehaviour
{
    private Vector3 startPlayerPosition;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public Vector3 mainOffset {  get; private set; }
    void Start()
    {
        startPlayerPosition = G.playerTransform.position;
    }
    void Update()
    {
        Vector3 cameraPosition = cinemachineCamera.State.GetFinalPosition();
        mainOffset = startPlayerPosition - cameraPosition;
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);
    }
}
