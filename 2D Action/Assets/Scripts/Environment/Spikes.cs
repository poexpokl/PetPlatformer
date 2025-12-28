using System;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public event Action PlayerOnSpike;
    [SerializeField] GameObject blackCanvas;
    //private Teleportation PlayerTeleportation; //возможно мне не нужна статика, т.к. все spike будут в одном объект
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage((Vector2)transform.position); //?
            Instantiate(blackCanvas);
            PlayerOnSpike?.Invoke();
        }
    }
}
