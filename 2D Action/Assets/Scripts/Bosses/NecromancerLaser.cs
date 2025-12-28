using System.Collections;
using UnityEngine;

public class NecromancerLaser : MonoBehaviour
{
    [SerializeField] private float activeteTimePart;
    private float activateTime;
    [SerializeField] private float animationSpeed;
    [SerializeField] private float startAnimationTime = 3f;
    public float AnimationTime {  get; private set; }
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        AnimationTime = startAnimationTime / animationSpeed;
        activateTime = AnimationTime * activeteTimePart;
        GetComponent<Animator>().speed = animationSpeed;
    }
    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(LaserActivating());
    }

    IEnumerator LaserActivating()
    {
        yield return new WaitForSeconds(activateTime);
        boxCollider.enabled = true;
        //здесь же будет звук
        yield return new WaitForSeconds(AnimationTime - activateTime);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        boxCollider.enabled = false;
    }

    public void ChangeAnimationSpeed(float newSpeed)
    {
        animationSpeed = newSpeed;
        AnimationTime = startAnimationTime / animationSpeed;
        activateTime = AnimationTime * activeteTimePart;
        GetComponent<Animator>().speed = animationSpeed;
    }
}
