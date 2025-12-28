using UnityEngine;

public class ParallaxChild : MonoBehaviour
{
    ParallaxMain parallaxMain;
    [SerializeField] float offsetRatio;
    void Start()
    {
        parallaxMain = GetComponentInParent<ParallaxMain>();
    }
    void Update()
    {
        Vector3 offset = parallaxMain.mainOffset * offsetRatio;
        transform.localPosition = new Vector3(offset.x, offset.y, transform.localPosition.z);
    }
}
