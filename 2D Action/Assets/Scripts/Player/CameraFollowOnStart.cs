using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowOnStart : MonoBehaviour
{
    CinemachinePositionComposer positionComposer;
    Vector3 damping;
    private void Start()
    {
        positionComposer = GetComponent<CinemachinePositionComposer>();
        damping = positionComposer.Damping;
        positionComposer.Damping = new Vector3(0, 0, damping.z);
        transform.position = G.playerTransform.position;
        StartCoroutine(ReturnDamping());
    }

    IEnumerator ReturnDamping()
    {
        yield return new WaitForNextFrameUnit();
        yield return new WaitForNextFrameUnit();
        positionComposer.Damping = damping;
    }
}
