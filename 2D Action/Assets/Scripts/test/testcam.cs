using UnityEngine;

public class testcam : MonoBehaviour
{
    private Transform t;

    private void Start()
    {
        t = G.playerTransform;
    }

    private void Update()
    {
        transform.position = new Vector3(t.position.x, t.position.y, transform.position.z);
    }
}
