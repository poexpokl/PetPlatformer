using System;
using UnityEngine;

public class ArenaEnter : MonoBehaviour
{
    public event Action OnArena;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            OnArena?.Invoke();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
