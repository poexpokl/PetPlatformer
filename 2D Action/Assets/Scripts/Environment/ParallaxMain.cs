using Unity.Cinemachine;
using UnityEngine;

public class ParallaxMain : MonoBehaviour
{
    private Vector3 startPlayerPosition;
    //[SerializeField] private Transform cameraTransform;
    //private ParallaxChild[] childrensScript;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public Vector3 mainOffset {  get; private set; }
    void Start()
    {
        startPlayerPosition = G.playerTransform.position;
        //childrensScript = GetComponentsInChildren<ParallaxChild>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = cinemachineCamera.State.GetFinalPosition();
        mainOffset = startPlayerPosition - cameraPosition;
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);
        /*foreach (ParallaxChild childScript in childrensScript)
        { 
            childScript.UpdatePosition();
        }*/
    }

    //в случае когда камера сменилась, запустить событие смены камеры и сюда записать новую
}
