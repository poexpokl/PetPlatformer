using System.Collections.Generic;
using UnityEngine;

public enum PlayerTriggerType 
{
    Right,
    Left, 
    Up, 
    Down
}

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private string[] enemyTags = { "Enemy"};
    [SerializeField] private float damage;
    [SerializeField] PlayerTriggerType triggerType;
    private PlayerController playerController;
    [SerializeField] private AudioClip enemyHitAudio;
    [SerializeField] private AudioClip spikeHitAudio;
    private AudioSource audioSource;

    private HashSet<GameObject> alreadyHit = new HashSet<GameObject>();

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsValidTarget(other.gameObject) && !alreadyHit.Contains(other.gameObject))
        {
            alreadyHit.Add(other.gameObject);
            other.gameObject.GetComponent<IDamageble>().GetDamage(damage, triggerType);
            if (other.gameObject.tag == "Enemy")
            {
                playerController.EnemyAttacked(triggerType);
                audioSource.clip = enemyHitAudio;
                audioSource.Play();
            }
        }
        else if (((1 << other.gameObject.layer) & environmentLayer) != 0)
        {
            playerController.EnvironmentAttacked(triggerType);
            audioSource.clip = spikeHitAudio;
            audioSource.Play();
        }
    }

    private void OnDisable()
    {
        audioSource.clip = null;
        alreadyHit.Clear();
    }
    private bool IsValidTarget(GameObject target)
    {
        foreach (string tag in enemyTags)
        {
            if (target.CompareTag(tag))
                return true;
        }

        return false;
    }
}
